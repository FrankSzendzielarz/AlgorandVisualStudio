﻿using AlgoStudio.Compiler.CompiledCodeModel;
using AlgoStudio.Compiler.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgoStudio.Compiler.Operators
{

    internal class ShortOperatorCodeGenerator : NumericCodeGenerator
    {
        const ulong mask = 0x_00000000_0000ffff;
        const ulong shiftOperatorCountLimiter = 0x_00_00_00_00_00_00_00_1f; //C# lang specification restricts int or int convertible types to lower 5 bits of shift count
        const ulong signBit = 0x_00000000_00008000;
        const ulong int32NegativeMask = 0x_0000_0000_ffff_0000;
        const ulong int64NegativeMask = 0x_ffff_ffff_ffff_0000;

        public ShortOperatorCodeGenerator(CodeBuilder code) : base(code)
        {
        }
        internal override void LogicalNegate()
        {
            throw new ErrorDiagnosticException("E017");
        }

        internal override void Increment()
        {
            _code.int_literal_constant(1);
            _code.plus();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
        }

        internal override void Decrement()
        {
            var zero = _code.ReserveLabel();
            var end = _code.ReserveLabel();
            _code.dup();
            _code.bz(zero);
            _code.int_literal_constant(1);
            _code.minus();
            _code.b(end);
            _code.AddLabel(zero);
            _code.pop();
            _code.int_literal_constant(mask);
            _code.AddLabel(end);
        }


        internal override void Negate()
        {
            string ignoreZeroLabel = _code.ReserveLabel();
            _code.dup();                         //keep the value on the stack
            _code.bz(ignoreZeroLabel);           //pop value and skip if zero, to avoid overflow
            _code.bitwise_not();                 //one's complement (invert) all bits
            _code.int_literal_constant(1);       //push a one
            _code.plus();                        //add the 1 and the inverted value - this yields the 2s complement negation - no overflow should occur
            _code.AddLabel(ignoreZeroLabel);     //add the reserved label here for the above branch
        }

        internal override void OnesComplement()
        {
            _code.bitwise_not();
        }
        internal override void Addition()
        {
            //simple two's complement addition
            _code.addw();
            _code.swap();
            _code.pop();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
        }

        internal override void BitwiseAnd()
        {
            _code.bitwise_and();
        }

        internal override void BitwiseOr()
        {
            _code.bitwise_or();
        }
        internal override void BitwiseXor()
        {
            _code.bitwise_xor();

        }
        internal override void Division()
        {
            //TODO make this a PredefinedSubroutine
            //check signs
            _code.dup2();
            _code.bitwise_xor();
            _code.int_literal_constant(15);
            _code.getbit();
            _code.cover(2);

            //divisor / denominator
            _code.dup();
            _code.int_literal_constant(15);
            _code.getbit();
            string positiveDivisor = _code.ReserveLabel();
            _code.bz(positiveDivisor);
            //negate divisor
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.addw();
            _code.swap();
            _code.pop();

            _code.AddLabel(positiveDivisor);

            //dividend / numerator
            _code.dig(1);
            _code.int_literal_constant(15);
            _code.getbit();
            string positiveDividend = _code.ReserveLabel();
            _code.bz(positiveDividend);
            //negate divided
            _code.swap();
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.addw();
            _code.swap();
            _code.pop();
            _code.swap();

            _code.AddLabel(positiveDividend);

            //do the division
            _code.divide();

            //check if sign should be swapped
            _code.swap();
            string exit = _code.ReserveLabel();
            _code.bz(exit);
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.plus();

            _code.AddLabel(exit);

        }
        internal override void GreaterThan()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(15);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.greater_than();
        }

        internal override void GreaterThanOrEqual()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(15);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.greater_than_or_equals();
        }
        internal override void LessThan()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(15);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.less_than();
        }

        internal override void LessThanOrEqual()
        {
            _code.dup2();                           //A B A B
            _code.bitwise_xor();                    //A B sign-in-top-bit
            _code.int_literal_constant(15);
            _code.getbit();                       //A B sign
            string compare = _code.ReserveLabel();    //
            _code.bz(compare);                      //A B  (if same sign, just compare)
            _code.swap();                           //B A  (if different sign, then largest(neg) is lowest)
            _code.AddLabel(compare);
            _code.less_than_or_equals();
        }
        internal override void LeftShift()
        {
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();
            _code.shl();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
        }

        internal override void Multiplication()
        {
            _code.mulw();
            _code.swap();
            _code.pop();
            _code.int_literal_constant(mask);
            _code.bitwise_and();
        }

        internal override void Remainder()
        {
            //dup A%B 
            _code.dup2();                                           //A B A B <-HEAD
            Division();                                             //A B (A/B) <-HEAD
            _code.dup();                                            //A B (A/B) (A/B) <-HEAD
            string exitAsZero = _code.ReserveLabel();
            string exit = _code.ReserveLabel();
            //A must have been zero
            _code.bz(exitAsZero);                                   //A B (A/B) <-HEAD

            //multiply result by B, 
            _code.mulw();                                           //A OVERFLOW (B*(A/B)) <-HEAD
            _code.swap();                                           //A (B*(A/B)) OVERFLOW  <-HEAD
            _code.pop();                                            //A (B*(A/B))   <-HEAD

            //A-B to get remainder, so get -B
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.plus();                                           //A -(B*(A/B))   <-HEAD

            _code.addw();                                           //OVERFLOW (A-(B*(A/B)) <-HEAD
            _code.swap();
            _code.pop();
            _code.b(exit);

            _code.AddLabel(exitAsZero);
            _code.pop();                                            //A B  <-HEAD
            _code.pop();                                            //A <-HEAD (where A must be zero)


            _code.AddLabel(exit);
        }

        internal override void RightShift()
        {
            _code.int_literal_constant(shiftOperatorCountLimiter);
            _code.bitwise_and();
            _code.shr();
        }

        internal override void Subtraction()
        {
            string exit = _code.ReserveLabel();
            _code.dup();
            _code.bz(exit);

            //two's complement
            _code.bitwise_not();
            _code.int_literal_constant(1);
            _code.plus();

            _code.addw();
            _code.swap();

            _code.int_literal_constant(mask);
            _code.bitwise_and();

            _code.AddLabel(exit);
            _code.pop();
        }

        internal override void CastToDecimal()
        {
            _code.dup();
            _code.int_literal_constant(0x8000);
            _code.bitwise_and();

            CastLongAndSignByteToDecimal();
        }
        internal override void CastToObject()
        {
            //nop
        }

        internal override void CastToByte()
        {
            _code.int_literal_constant(0xff);
            _code.bitwise_and();
        }

        internal override void CastToInt()
        {
            var exit = _code.ReserveLabel();
            _code.dup();
            _code.int_literal_constant(signBit);
            _code.bitwise_and();
            _code.bz(exit);//positive
            _code.int_literal_constant(int32NegativeMask);//promote to int32 
            _code.bitwise_or();
            _code.AddLabel(exit);
        }

        internal override void CastToLong()
        {
            var exit = _code.ReserveLabel();
            _code.dup();
            _code.int_literal_constant(signBit);
            _code.bitwise_and();
            _code.bz(exit);//positive
            _code.int_literal_constant(int64NegativeMask);//promote to int64
            _code.bitwise_or();
            _code.AddLabel(exit);
        }

        internal override void CastToSbyte()
        {
            _code.int_literal_constant(0xff);
            _code.bitwise_and();
        }

        internal override void CastToShort()
        {
            //nop
        }

        internal override void CastToUInt()
        {
            CastToInt();
        }

        internal override void CastToULong()
        {
            CastToLong();
        }

        internal override void CastToUShort()
        {
            //nop
        }
    }
}
