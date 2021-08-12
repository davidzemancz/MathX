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

        public DataTypeEnum DataType { get; set; }

        public enum DataTypeEnum
        {
            None = 0,
            Double = 1,
            Boolean = 2,
            Tenzor = 3,
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
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Double, "_", (double)a.Value + (double)b.Value);
            return null;
        }

        public static Variable operator -(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Double, "_", (double)a.Value - (double)b.Value);
            return null;
        }

        public static Variable operator *(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Double, "_", (double)a.Value * (double)b.Value);
            return null;
        }

        public static Variable operator /(Variable a, Variable b)
        {
            if (a.DataType == DataTypeEnum.Double && b.DataType == DataTypeEnum.Double)
                return new Variable(DataTypeEnum.Double, "_", (double)a.Value / (double)b.Value);
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

        public static implicit operator Variable(double number)
        {
            return new Variable(DataTypeEnum.Double, "_", number);
        }

        public override string ToString()
        {
            return Value?.ToString();
        }

    }
}
