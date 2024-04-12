using AlgoStudio.Core;
using static AlgoStudio.SpecFlow.Contracts.Libraries.LibraryTwo;

namespace AlgoStudio.SpecFlow.Contracts.Libraries
{

    internal class LibraryOne : SmartContractLibrary
    {


        public static int AddTwoNums(int a, int b)
        {

            return a + b;
        }

        public static ulong UseBaseMethod()
        {
            return CurrentApplicationID;

        }
        public static int AddTwoNumsWithDependency(int a, int b)
        {
            return AddThreeNums(a, b, 7);
        }

        public static bool Circular1(bool input)
        { 
            if (input) 
            {
                return Circular2(true);
            }
            else
            {
                return false;
            }
        }
    }
}
