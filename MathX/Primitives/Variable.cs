using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Variable
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public bool Temporary => Name.StartsWith("_");

        public DataTypeEnum DataType { get; set; }

        public enum DataTypeEnum
        {
            None = 0,
            Double = 1,
            Boolean = 2,
            Vector = 3,
        }

        public Variable()
        {

        }

        public Variable(DataTypeEnum dataType, string name)
        {
            DataType = dataType;
            Name = name;
        }

        public Variable(DataTypeEnum dataType, string name, object value) : this(dataType, name)
        {
            Value = value;
        }

        public static Variable operator +(Variable a, Variable b)
        {
            if (a?.DataType == DataTypeEnum.Vector && b?.DataType == DataTypeEnum.Vector)
                return new Variable(DataTypeEnum.Vector, "_", (Vector)a?.Value + (Vector)b?.Value);
            else if (a?.DataType == DataTypeEnum.Double || b?.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Double, "_", (a?.Value as double? ?? 0) + (b?.Value as double? ?? 0));
            return null;
        }

        public static Variable operator -(Variable a, Variable b)
        {
            if (a?.DataType == DataTypeEnum.Vector && b?.DataType == DataTypeEnum.Vector)
                return new Variable(DataTypeEnum.Vector, "_", (Vector)a?.Value - (Vector)b?.Value);
            else if (a?.DataType == DataTypeEnum.Double || b?.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Double, "_", (a?.Value as double? ?? 0) - (b?.Value as double? ?? 0));
            return null;
        }

        public static Variable operator *(Variable a, Variable b)
        {
            if (a?.DataType == DataTypeEnum.Vector && b?.DataType == DataTypeEnum.Vector)
                return new Variable(DataTypeEnum.Vector, "_", (Vector)a?.Value * (Vector)b?.Value);
            else if (a?.DataType == DataTypeEnum.Double || b?.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Double, "_", (a?.Value as double? ?? 0) * (b?.Value as double? ?? 0));
            return null;
        }

        public static Variable operator /(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double || b.DataType == DataTypeEnum.Double)
            {
                if ((double)b.Value == 0) throw new Exception("Unable to divide by zero");
                return new Variable(DataTypeEnum.Double, "_", (a.Value as double? ?? 1) * (b.Value as double? ?? 1));
            }
            return null;
        }

        public static Variable operator >(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Boolean, "_", (double)a.Value > (double)b.Value);
            return null;
        }

        public static Variable operator <(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Boolean, "_", (double)a.Value < (double)b.Value);
            return null;
        }

        public static Variable operator >=(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Boolean, "_", (double)a.Value >= (double)b.Value);
            return null;
        }

        public static Variable operator <=(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Boolean, "_", (double)a.Value <= (double)b.Value);
            return null;
        }

        public static Variable operator ==(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Boolean, "_", (double)a.Value == (double)b.Value);
            return null;
        }

        public static Variable operator !=(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Boolean, "_", (double)a.Value != (double)b.Value);
            return null;
        }

        public static implicit operator Variable(Vector vector)
        {
            return new Variable(DataTypeEnum.Vector, "_", vector);
        }

        public static implicit operator Variable(double number)
        {
            return new Variable(DataTypeEnum.Double, "_", number);
        }

        public override string ToString()
        {
            if (Temporary) return Value?.ToString();
            return $"{Name} = {Value?.ToString()}";
        }

    }
}
