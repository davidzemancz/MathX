using Base.Api;
using MathX.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Condition
    {
        public bool Result { get; set; }

        public string Expression { get; set; }

        public Condition()
        {

        }

        public Condition(string expression)
        {
            this.Expression = expression;
        }

        public bool Evaluate(Process process, out BaseStatus status)
        {
            Result = false;
            Variable variable = new Expression(process, Expression).Evaluate(out status);
            if (variable.DataType == Variable.DataTypeEnum.Double)
            {
                Result = (double)variable.Value > 0;
            }
            return Result;
        }
    }
}
