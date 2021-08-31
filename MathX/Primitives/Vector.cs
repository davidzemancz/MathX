using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Vector
    {
        public Variable[] Components { get; set; }

        public int Dimension => Components.Length;
                
        public Variable this[int i]
        {
            get => i < Dimension ? Components[i] : null;
            set => Components[i] = value;
        }
            

        public Vector(int dimension)
        {
            Components = new Variable[dimension];
        }

        public Vector(params Variable[] components)
        {
            Components = components;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            int dim = Math.Max(a.Dimension, b.Dimension);
            Vector c = new Vector(dim);
            for (int i = 0; i < dim; i++)
            {
                c[i] = a[i] + b[i];
            }
            return c;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            int dim = Math.Max(a.Dimension, b.Dimension);
            Vector c = new Vector(dim);
            for (int i = 0; i < dim; i++)
            {
                c[i] = a[i] - b[i];
            }
            return c;
        }

        public static Variable operator *(Vector a, Vector b)
        {
            int dim = Math.Max(a.Dimension, b.Dimension);
            Variable c = null;
            for (int i = 0; i < dim; i++)
            {
                c = c + (a[i] * b[i]);
            }
            return c;
        }

        public override string ToString()
        {
            string vector = "[";
            foreach (object component in Components)
            {
                vector += component.ToString() + ",";
            }
            if (Components.Length > 0) vector = vector.Substring(0, vector.Length - 1);
            vector += "]";
            return vector;
        }
    }
}
