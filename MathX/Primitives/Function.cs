using Base.Api;
using MathX.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX.Primitives
{
    public class Function
    {
        public const string Increment = "increment";
        public const string IncrementShort = "i";
        public const string Power = "power";
        public const string PowerShort = "pow";
        public const string Print = "print";
        public const string Sinus = "sin";

        private string _name;
        private Variable[] _parameters;
        private Process _process;

        public Function()
        {
        }

        public Function(Process process, string name, Variable[] parameters)
        {
            _process = process;
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

                    result = _parameters[0] + 1.0;
                }
                else if (_name == Power || _name == PowerShort)
                {
                    if (!ArgumentsValid(2)) return result;
                    else if (_parameters[0].DataType == Variable.DataTypeEnum.Double && _parameters[1].DataType == Variable.DataTypeEnum.Double)
                    {
                        result = Math.Pow((double)_parameters[0].Value, (double)_parameters[1].Value);
                    }
                    else throw new Exception($"Both arguments of {_name} function must be numbers");
                }
                else if (_name == Sinus)
                {
                    if (!ArgumentsValid(1)) return result;
                    else if (_parameters[0].DataType == Variable.DataTypeEnum.Double)
                    {
                        result = Math.Sin((double)_parameters[0].Value);
                    }
                    else throw new Exception($"First argument of {_name} function must be a number");
                }
                else if(_name == Print)
                {
                    if (!ArgumentsValid(1)) return result;
                    _process.WriteToOutput(_parameters[0].Value?.ToString());
                }
                else
                {
                    throw new Exception($"Invalid function name {_name}");
                }
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Invalid function call] {ex.Message}", ex);
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
