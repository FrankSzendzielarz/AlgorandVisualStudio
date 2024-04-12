
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol;
using Microsoft.VisualStudio.Shared.VSCodeDebugProtocol.Messages;

using static System.FormattableString;

namespace TEALDebugAdapterComponent
{
    public abstract class TealDebugAdapterVariable : TealDebugAdapterObject<Variable, ValueFormat>, IVariableContainer
    {
        #region Constructor

        protected TealDebugAdapterVariable(TealDebugAdapter adapter, string name, string type)
            : base(adapter)
        {
            this.Name = name;
            this.Type = type;
        }

        #endregion

        #region SampleObject Implementation

        public override void Invalidate()
        {
            base.Invalidate();

            if (this.GetChildren(false) != null)
            {
                foreach (TealDebugAdapterVariable variable in this.GetChildren())
                {
                    variable.Invalidate();
                }
            }
        }

        protected override Variable CreateProtocolObject()
        {
            ValueFormat format = this.Format;

            VariablePresentationHint presentationHint = new VariablePresentationHint();
            if (this.IsReadOnly)
            {
                presentationHint.Attributes = VariablePresentationHint.AttributesValue.ReadOnly;
            }
            else
            {
                presentationHint.Attributes = VariablePresentationHint.AttributesValue.None;
            }

            return new Variable(
                name: this.Name,
                value: this.GetValue(format?.Hex ?? false),
                type: this.Type,
                variablesReference: (this.GetChildren() != null && this.GetChildren().Any()) ? this.Adapter.GetNextId() : 0,
                evaluateName: this.GetEvaluateName(),
                presentationHint: presentationHint);
        }

        internal string GetEvaluateName()
        {
            // Purposefully using a weird format to avoid looking like other languages
            string evalName = this.Name;
            IVariableContainer container = this.Container;

            while (container != null)
            {
                evalName = Invariant($"{container.Name}~{evalName}");
                container = container.Container;
            }

            return evalName;
        }

        #endregion

        public string Type { get; set; }

        internal void SetContainer(IVariableContainer container)
        {
            // Variables can be moved to a different scope under some conditions (e.g. if the Globals scope is disabled), but
            //  we want them to remain associated with their original container to avoid conlficts
            if (this.Container == null)
            {
                this.Container = container;
            }
        }

        #region Abstract Members

        public IVariableContainer Container { get; private set; }

        public abstract bool IsReadOnly { get; }

        public abstract string GetValue(bool showInHex);
        public abstract void SetValue(string value);
        protected abstract IReadOnlyCollection<TealDebugAdapterVariable> GetChildren(bool shouldCreate = true);

        protected override bool IsSameFormat(ValueFormat a, ValueFormat b)
        {
            if (Object.ReferenceEquals(null, a) || Object.ReferenceEquals(null, b))
                return Object.ReferenceEquals(a, b);
            return a.Hex == b.Hex;
        }

        #endregion

        #region ISampleVariableContainer Implementation

        public string Name { get; private set; }

        public int VariableReference
        {
            get
            {
                return this.ProtocolObject?.VariablesReference ?? 0;
            }
        }

        public IReadOnlyCollection<IVariableContainer> ChildContainers
        {
            get { return this.GetChildren(); }
        }

        public IReadOnlyCollection<TealDebugAdapterVariable> Variables
        {
            get { return this.GetChildren(); }
        }

        public VariablesResponse HandleVariablesRequest(VariablesArguments args)
        {
            return new VariablesResponse(variables: this.GetChildren().Any() ? this.GetChildren().Select(c => c.GetProtocolObject(args.Format)).ToList() : null);
        }

        public SetVariableResponse HandleSetVariableRequest(SetVariableArguments args)
        {
            return null;
        }

        #endregion
    }

    public class SimpleVariable : TealDebugAdapterVariable
    {
        #region Private Fields
        public object underlyingValue;
        private string value;
        private List<TealDebugAdapterVariable> children;

        #endregion

        #region Constructor

        public SimpleVariable(TealDebugAdapter adapter, string name, string type, string value, object underlyingValue)
            : base(adapter, name, type)
        {
            this.value = value;

            this.children = new List<TealDebugAdapterVariable>();
            this.underlyingValue = underlyingValue;
        }

        #endregion

        #region SampleVariable Implementation

        public override bool IsReadOnly
        {
            get { return false; }
        }

        internal static string ShowAsHex(bool showInHex, string value)
        {
            int valueAsInt;
            if (showInHex && Int32.TryParse(value, out valueAsInt))
            {
                return Invariant($"0x{valueAsInt:X8}");
            }
            return value;
        }

        public override string GetValue(bool showInHex)
        {
            return ShowAsHex(showInHex, this.value);
        }

        public override void SetValue(string value)
        {
            this.value = value;
        }

        protected override IReadOnlyCollection<TealDebugAdapterVariable> GetChildren(bool shouldCreate = true)
        {
            return this.children;
        }

        #endregion

        internal void AddChild(TealDebugAdapterVariable variable)
        {
            this.children.Add(variable);
            variable.SetContainer(this);
        }
    }

    internal class WrapperVariable : TealDebugAdapterVariable
    {
        #region Private Fields

        private Func<string> valueGetter;
        private Func<IReadOnlyCollection<TealDebugAdapterVariable>> childrenGetter;

        private IReadOnlyCollection<TealDebugAdapterVariable> _children;

        #endregion

        #region Constructor

        public WrapperVariable(TealDebugAdapter adapter, string name, string type, Func<string> valueGetter, Func<IReadOnlyCollection<TealDebugAdapterVariable>> childrenGetter = null)
            : base(adapter, name, type)
        {
            this.valueGetter = valueGetter;
            this.childrenGetter = childrenGetter;
        }

        #endregion

        #region SampleVariable Implementation

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override string GetValue(bool showInHex)
        {
            return this.valueGetter();
        }

        public override void SetValue(string value)
        {
            throw new ProtocolException("Wrapper variables are read only!");
        }

        protected override IReadOnlyCollection<TealDebugAdapterVariable> GetChildren(bool shouldCreate = true)
        {
            if (this._children == null && this.childrenGetter != null && shouldCreate)
            {
                this._children = this.childrenGetter();

                if (this._children != null)
                {
                    foreach (TealDebugAdapterVariable child in this._children)
                    {
                        child.SetContainer(this);
                    }
                }
            }

            return this._children;
        }

        #endregion

        #region SampleObject Implementation

        public override void Invalidate()
        {
            base.Invalidate();

            this._children = null;
        }

        #endregion
    }
}
