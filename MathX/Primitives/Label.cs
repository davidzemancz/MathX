using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Label
    {
        public long Position { get; set; }

        public string Name { get; set; }

        public Label()
        {

        }
        public Label(long position, string name)
        {
            Position = position;
            Name = name;
        }
    }
}
