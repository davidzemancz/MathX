using Base.Api;
using MathX.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public const string Sqrt = "sqrt";

        private string _name;
        private string _expression;
        private string[] _parametersNames;
        private Variable[] _parameters;
        private Process _process;

        public Function()
        {
        }

        public Function(Process process, string name, string expression, string[] parametersNames)
        {
            _process = process;
            _name = name.ToLower();
            _expression = expression;
            _parametersNames = parametersNames;
        }

        public Function(Process process, string name, Variable[] parameters)
        {
            _process = process;
            _name = name.ToLower();
            _parameters = parameters;
        }

        public Variable Call(out BaseStatus status)
        {
            return Call(_parameters, out status);
        }

        public Variable Call(Variable[] parameters, out BaseStatus status)
        {
            _parameters = parameters;
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
                else if (_name == Print)
                {
                    if (!ArgumentsValid(1)) return result;
                    string exprStr = _parameters[0].Value?.ToString();
                    Expression expression = new Expression(_process, exprStr);
                    Variable expResult = expression.Evaluate(out status);
                    status.ThrowIfError();
                    _process.PushOutput(expResult.Value?.ToString());
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
                else if(_name == Sqrt)
                {
                    if (!ArgumentsValid(1)) return result;
                    if (_parameters[0].DataType == Variable.DataTypeEnum.Double)
                    {
                        result = Math.Sqrt((double)_parameters[0].Value);
                    }
                    else throw new Exception($"Argument of {_name} function must be number");
                }
                else if (_process.Functions.ContainsKey(_name))
                {
                    if (!ArgumentsValid(_parametersNames.Length)) return result;

                    // Save function params to process variables
                    Dictionary<string, Variable> tempVariablesStorage = new Dictionary<string, Variable>();
                    int i = 0;
                    foreach(string parameterName in _parametersNames)
                    {
                        if (_process.Variables.ContainsKey(parameterName)) 
                        {
                            tempVariablesStorage[parameterName] = _process.Variables[parameterName];
                        }
                        _process.Variables[parameterName] = _parameters[i++];
                    }

                    // Evaluate function expression
                    result = new Expression(_process, _expression).Evaluate(out status);
                    status.ThrowIfError();

                    // Restore process variables
                    foreach(KeyValuePair<string, Variable> kvp in tempVariablesStorage)
                    {
                        _process.Variables[kvp.Key] = kvp.Value;
                    }
                }
                else 
                {
                    if (!string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_expression))
                    {
                        _process.Functions[_name] = this;
                        result = _process.Functions[_name].Call(out status);
                    }
                    else
                    {
                        throw new Exception($"Invalid function name {_name}");
                    }
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

        public override string ToString()
{
            string paramsNames = "";
            foreach (string paramName in _parametersNames)
            {
                paramsNames += paramName + ",";
            }
            if (paramsNames.Length > 0) paramsNames = paramsNames.Substring(0, paramsNames.Length - 1);
            return $"{_name}({paramsNames}) = {_expression}";
        }
    }
}
