using MathX.Primitives.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Vector : IVariableValue
    {
        public string Type => nameof(Vector);

        public Variable[] Components { get; set; }

        public int Dimension => Components.Length;
                
        public Variable this[int index]
        {
            get => index < Dimension ? Components[index] :  new Variable();
            set
            {
                Components[index].DataType = value.DataType;
                Components[index].Name = "_";
                Components[index].Value = value.Value;
            }
        }
           
        public Vector()
        {
            Components = new Variable[0];
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
            Variable c = new Variable();
            for (int i = 0; i < dim; i++)
            {
                c = c + (a[i] * b[i]);
            }
            return c;
        }

        public override string ToString()
        {
            string vector = "[";
            foreach (Variable component in Components)
            {
                vector += component.ToString() + ",";
            }
            if (Components.Length > 0) vector = vector.Substring(0, vector.Length - 1);
            vector += "]";
            return vector;
        }
    }
}
