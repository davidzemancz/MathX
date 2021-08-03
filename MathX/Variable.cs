using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX
{
    public class Variable
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public Variable(string name)
        {
            Name = name;
        }

        public Variable(string name, object value) : this(name)
        {
            Value = value;
        }
    }
}
