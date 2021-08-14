using Base.Api;
using MathX.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Loop
    {
        public long Position { get; set; }

        public bool Result { get; set; }

        public string Expression { get; set; }
        
        public string Keyword { get; set; }

        public Loop() 
        { 

        }

        public Loop(long position, string keyword, string expression)
        {
            Position = position;
            Keyword = keyword;
            Expression = expression;

        }

        public bool EvaluateCondition(Process process, out BaseStatus status)
        {
            Result = new Condition(Keywords.If, Expression).Evaluate(process, out status);
            return Result;
        }
    }
}
