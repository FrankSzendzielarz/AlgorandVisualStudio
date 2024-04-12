
// Licensed under the MIT License.

namespace TEALDebugAdapterComponent
{
    public abstract class TealDebugAdapterObject<TProtocolObject, TFormat>
        where TProtocolObject : class
        where TFormat : class
    {
        protected TealDebugAdapterObject(TealDebugAdapter adapter)
        {
            this.Adapter = adapter;
        }

        protected TealDebugAdapter Adapter { get; private set; }

        protected abstract bool IsSameFormat(TFormat a, TFormat b);

        protected abstract TProtocolObject CreateProtocolObject();

        protected TProtocolObject ProtocolObject { get; private set; }

        protected TFormat Format { get; private set; }

        public virtual void Invalidate()
        {
            this.Format = null;
            this.ProtocolObject = null;
        }

        public TProtocolObject GetProtocolObject(TFormat format)
        {
            if (this.ProtocolObject == null || !this.IsSameFormat(format, this.Format))
            {
                this.Format = format;
                this.ProtocolObject = this.CreateProtocolObject();
            }

            return this.ProtocolObject;
        }
    }
}
