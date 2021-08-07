using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX
{
    public class Function
    {
        public const string Increment = "$increment";
        public const string IncrementShort = "$i";
        public const string Power = "$power";
        public const string PowerShort = "$pow";
        public const string Sinus = "$sin";

        private string _name;
        private Variable[] _parameters;

        public Function(string name, Variable[] parameters)
        {
            _name = name.ToLower();
            _parameters = parameters;
        }

        public Variable Call()
        {
            Variable result = new Variable(Variable.DataTypeEnum.None, "_");
             
            if (_name == Increment || _name == IncrementShort)
            {
                if (!ArgumentsValid(1)) return result;

                result = Operation.Add(_parameters[0], 1.0);
            }
            else if (_name == Power || _name == PowerShort)
            {
                if (!ArgumentsValid(2)) return result;

                result = Operation.Power(_parameters[0], _parameters[1]);
            }
            else if (_name == Sinus)
            {
                if (!ArgumentsValid(1)) return result;

                if (_parameters[0].DataType == Variable.DataTypeEnum.Double)
                {
                    result = Math.Sin((double)_parameters[0].Value);
                }
                else throw new Exception($"First argument of {_name} function must be a number");
            }
            else
            {
                throw new Exception($"Invalid function name {_name}");
            }
            return result;
        }

        private bool ArgumentsValid(int minimumAllowedArgs, int maximumAllowedArgs = -1)
        {
            if (maximumAllowedArgs == -1) maximumAllowedArgs = minimumAllowedArgs;
            if (_parameters.Length < minimumAllowedArgs || _parameters.Length > maximumAllowedArgs)
            {
                throw new Exception($"Invalid arguments for function {_name}");
            }
            return true;
        }
    }
}
