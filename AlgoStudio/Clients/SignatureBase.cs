using Algorand;
using AlgoStudio.Compiler;
using System.Collections.Generic;
using System.Linq;

namespace AlgoStudio
{




    public class SignatureBase
    {

        LogicsigSignature smartSig;


        public SignatureBase(LogicsigSignature smartSig)
        {
            this.smartSig = smartSig;
        }

        private List<byte[]> toByteArrays(List<object> args)
        {
            return args.Select(a => TealTypeUtils.EncodeArgument(a)).ToList();
        }
        protected void UpdateSmartSignature(List<object> args)
        {
            if (args != null && args.Count > 0)
            {
                smartSig.Args = toByteArrays(args);
            }

        }



    }
}
