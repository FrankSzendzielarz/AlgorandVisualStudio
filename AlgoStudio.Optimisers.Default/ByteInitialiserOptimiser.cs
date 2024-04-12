using System;
using System.Collections.Generic;
using System.Linq;
using AlgoStudio.Optimisers;


namespace AlgoStudio.Optimisers.Default
{

    public class RedundantBytesDeclarationOptimiser : IOptimiser
    {
        public void ChildScopeEntered()
        {
            //do nothing
        }

        public void ChildScopeExited()
        {
            //do nothing
        }



        public void LineAdded(IEnumerable<CompiledLine> codeBlockLines, ICompilerMemento compiler)
        {
            List<CompiledLine> lines = codeBlockLines.ToList();

            lines.Reverse();
        
            RedundantBytesOptimisation(compiler, lines);

        }

      

        private static List<string> redundantBytesSequence = new List<string>() { "concat", "byte", "byte" };
        private static void RedundantBytesOptimisation(ICompilerMemento compiler, List<CompiledLine> lines)
        {
            if (lines.Count >= redundantBytesSequence.Count)
            {
                if (lines.Take(redundantBytesSequence.Count).Select(ocs => ocs.Opcode).SequenceEqual(redundantBytesSequence) &&
                    lines[2].Parameters[0] == @""""""""""
                    )
                {

                    compiler.RemoveLineAt(lines.Count - 3);
                    compiler.RemoveTopLine();
                }
            }
        }
    }
    public class MultipleBytesDeclarationOptimiser : IOptimiser
    {
        public void ChildScopeEntered()
        {
            //do nothing
        }

        public void ChildScopeExited()
        {
            //do nothing
        }



        public void LineAdded(IEnumerable<CompiledLine> codeBlockLines, ICompilerMemento compiler)
        {
            List<CompiledLine> lines = codeBlockLines.ToList();

            lines.Reverse();
         
            MultipleBytesDeclarationOptimisation(compiler, lines);
            MultipleBytesDeclarationOptimisation2(compiler, lines);


        }


        private static List<string> multipleBytesSequence = new List<string>() { "concat", "extract", "itob", "int", "concat", "byte" };
        private static List<string> multipleBytesSequence2 = new List<string>() { "concat", "extract", "itob", "int", "byte" };
        private static void MultipleBytesDeclarationOptimisation(ICompilerMemento compiler, List<CompiledLine> lines)
        {
            if (lines.Count >= multipleBytesSequence.Count)
            {
                if (lines.Take(multipleBytesSequence.Count).Select(ocs => ocs.Opcode).SequenceEqual(multipleBytesSequence) &&
                    lines[1].Line.Trim() == "extract 7 0" &&
                    lines[5].Parameters[0].StartsWith("0x")
                    )
                {

                    var byte1 = lines[3].Parameters[0];
                    var bytes = lines[5].Parameters[0];
                    var bval1 = int.Parse(byte1).ToString("X2");

                    var res = $"{bytes}{bval1}";

                    foreach (var _ in multipleBytesSequence) compiler.RemoveTopLine();

                    compiler.AddLine(new CompiledLine()
                    {
                        Opcode = "byte",
                        Line = $"byte {res}",
                        Optimisable = true,
                        Parameters = new string[] { res }
                    });

                    compiler.AddLine(new CompiledLine()
                    {
                        Opcode = "concat",
                        Line = $"concat",
                        Optimisable = true,
                    });
                }
            }
        }
        private static void MultipleBytesDeclarationOptimisation2(ICompilerMemento compiler, List<CompiledLine> lines)
        {
            if (lines.Count >= multipleBytesSequence.Count)
            {
                if (lines.Take(multipleBytesSequence2.Count).Select(ocs => ocs.Opcode).SequenceEqual(multipleBytesSequence2) &&
                    lines[1].Line.Trim() == "extract 7 0" &&
                    lines[4].Parameters[0].StartsWith("0x")
                    )
                {

                    var byte1 = lines[3].Parameters[0];
                    var bytes = lines[4].Parameters[0];
                    var bval1 = int.Parse(byte1).ToString("X2");

                    var res = $"{bytes}{bval1}";

                    foreach (var _ in multipleBytesSequence2) compiler.RemoveTopLine();

                    compiler.AddLine(new CompiledLine()
                    {
                        Opcode = "byte",
                        Line = $"byte {res}",
                        Optimisable = true,
                        Parameters = new string[] { res }
                    });

                 
                }
            }
        }


    }


    public class DoubleByteDeclarationOptimiser : IOptimiser
    {
        public void ChildScopeEntered()
        {
            //do nothing
        }

        public void ChildScopeExited()
        {
            //do nothing
        }

      

        public void LineAdded(IEnumerable<CompiledLine> codeBlockLines, ICompilerMemento compiler)
        {
            List<CompiledLine> lines = codeBlockLines.ToList();

            lines.Reverse();
            DoubleByteDeclarationOptimisation(compiler, lines);
        
        }

        private static List<string> doubleBytesSequence = new List<string>() { "concat", "extract", "itob", "int", "concat", "extract", "itob", "int" };
        private static void DoubleByteDeclarationOptimisation(ICompilerMemento compiler, List<CompiledLine> lines)
        {
            if (lines.Count >= doubleBytesSequence.Count)
            {
                if (lines.Take(doubleBytesSequence.Count).Select(ocs => ocs.Opcode).SequenceEqual(doubleBytesSequence) &&
                    lines[1].Line.Trim() == "extract 7 0" &&
                    lines[5].Line.Trim() == "extract 7 0")
                {

                    var byte1 = lines[3].Parameters[0];
                    var byte2 = lines[7].Parameters[0];
                    int bval1 = int.Parse(byte1);
                    int bval2 = int.Parse(byte2);
                    var res = $"0x{bval2.ToString("X2")}{bval1.ToString("X2")}";

                    foreach (var _ in doubleBytesSequence) compiler.RemoveTopLine();

                    compiler.AddLine(new CompiledLine()
                    {
                        Opcode = "byte",
                        Line = $"byte {res}",
                        Optimisable = true,
                        Parameters = new string[] { res }
                    });

                    compiler.AddLine(new CompiledLine()
                    {
                        Opcode = "concat",
                        Line = $"concat",
                        Optimisable = true,
                    });
                }
            }
        }

   
    }
}
