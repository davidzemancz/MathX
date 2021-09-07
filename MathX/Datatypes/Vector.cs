using System;
using MathX.Primitives;
using MathX.Primitives.Interfaces;

namespace MathX.Datatypes
{
    public class Vector : IVariableValue
    {
        #region PROPS
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

        #endregion

        #region CONSTRUCTORS

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

        #endregion

        #region PUBLIC METHODS
        
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

        #endregion

        #region OPERATORS

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

        #endregion

    }
}
