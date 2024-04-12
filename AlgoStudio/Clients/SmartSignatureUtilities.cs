using Algorand;
using Algorand.Algod;
using Algorand.Algod.Model;
using AlgoStudio.ABI;
using AlgoStudio;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AlgoStudio.Clients
{
    public static class SmartSignatureUtilities
    {

        public static async Task<LogicsigSignature> Compile(this Core.ICompiledSignature signature,  DefaultApi api)
        {
            using (var datams = new MemoryStream(Encoding.UTF8.GetBytes(signature.Program)))
            {
                CompileResponse response = await api.TealCompileAsync(datams);
                TEALProgram program=new TEALProgram(response.Result);
                return new LogicsigSignature(program.Bytes);
            }
        }

        
    }
}
