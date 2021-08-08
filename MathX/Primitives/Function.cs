using Base.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
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

        public Function()
        {
        }

        public Function(string name, Variable[] parameters)
        {
            _name = name.ToLower();
            _parameters = parameters;
        }

        public Variable Call(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            Variable result = new Variable(Variable.DataTypeEnum.None, "_");
            try
            { 
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
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Invalid statement] {ex.Message}", ex);
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
