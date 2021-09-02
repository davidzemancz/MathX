using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives.Utils
{
    public class StatementInfo
    {
        public Label Label { get; set; }
        
        public Condition Condition { get; set; }
        
        public Loop Loop { get; set; }

    }
}
