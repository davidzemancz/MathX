using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Operation
    {

        public static Variable Add(Variable addend1, Variable addend2)
        {
            if (addend1.DataType == Variable.DataTypeEnum.Double && addend2.DataType == Variable.DataTypeEnum.Double)
                return addend1 + addend2;
            return null;
        }

        public static Variable Subtract(Variable minued, Variable subtrahend)
        {
            if (minued.DataType == Variable.DataTypeEnum.Double && subtrahend.DataType == Variable.DataTypeEnum.Double)
                return minued - subtrahend;
            return null;
        }

        public static Variable Multiply(Variable factor1, Variable factor2)
        {
            if (factor1.DataType == Variable.DataTypeEnum.Double && factor2.DataType == Variable.DataTypeEnum.Double)
                return factor1 * factor2;
            return null;
        }

        public static Variable Divide(Variable dividend, Variable divisor)
        {
            if (dividend.DataType == Variable.DataTypeEnum.Double && divisor.DataType == Variable.DataTypeEnum.Double)
                return dividend / divisor;
            return null;
        }

        public static Variable Power(Variable number, Variable exponent)
        {
            if (number.DataType == Variable.DataTypeEnum.Double && exponent.DataType == Variable.DataTypeEnum.Double)
                return new Variable(Variable.DataTypeEnum.Double, "_", MathX.Pwr((double)number.Value, (double)exponent.Value));
            return null;
        }
    }
}
