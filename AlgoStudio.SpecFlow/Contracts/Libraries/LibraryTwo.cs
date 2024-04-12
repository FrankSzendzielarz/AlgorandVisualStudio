using AlgoStudio.Core;
using static AlgoStudio.SpecFlow.Contracts.Libraries.LibraryOne;

namespace AlgoStudio.SpecFlow.Contracts.Libraries
{
    internal class LibraryTwo : SmartContractLibrary
    {
        public static int AddThreeNums(int a, int b, int c)
        {
            return a + b + c;
        }

        public static bool Circular2(bool input)
        {
             
            return Circular1(!input);
        }
    }
}
