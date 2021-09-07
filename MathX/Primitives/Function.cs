using Base.Api;
using MathX.Processes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
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

        public string Name { get; set; }
        public string Expression { get; set; }
        public string[] ParametersNames { get; set; }
      
        [JsonIgnore]
        public Process Process { get; set; }
        
        private Variable[] _parameters;

        public Function()
        {
        }

        public Function(Process process, string name, string expression, string[] parametersNames)
        {
            Process = process;
            Name = name.ToLower();
            Expression = expression;
            ParametersNames = parametersNames;
        }

        public Function(Process process, string name, Variable[] parameters)
        {
            Process = process;
            Name = name.ToLower();
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
                if (Name == Increment || Name == IncrementShort)
                {
                    if (!ArgumentsValid(1)) return result;

                    result = _parameters[0] + 1.0;
                }
                else if (Name == Power || Name == PowerShort)
                {
                    if (!ArgumentsValid(2)) return result;
                    else if (_parameters[0].DataType == Variable.DataTypeEnum.Double && _parameters[1].DataType == Variable.DataTypeEnum.Double)
                    {
                        result = Math.Pow((double)_parameters[0].Value, (double)_parameters[1].Value);
                    }
                    else throw new Exception($"Both arguments of {Name} function must be numbers");
                }
                else if (Name == Print)
                {
                    if (!ArgumentsValid(1)) return result;
                    string exprStr = _parameters[0].Value?.ToString();
                    Expression expression = new Expression(Process, exprStr);
                    Variable expResult = expression.Evaluate(out status);
                    status.ThrowIfError();
                    Process.PushOutput(expResult.Value?.ToString());
                }
                else if (Name == Sinus)
                {
                    if (!ArgumentsValid(1)) return result;
                    else if (_parameters[0].DataType == Variable.DataTypeEnum.Double)
                    {
                        result = Math.Sin((double)_parameters[0].Value);
                    }
                    else throw new Exception($"First argument of {Name} function must be a number");
                }
                else if(Name == Sqrt)
                {
                    if (!ArgumentsValid(1)) return result;
                    if (_parameters[0].DataType == Variable.DataTypeEnum.Double)
                    {
                        result = Math.Sqrt((double)_parameters[0].Value);
                    }
                    else throw new Exception($"Argument of {Name} function must be number");
                }
                else if (Process.Functions.ContainsKey(Name))
                {
                    if (!ArgumentsValid(ParametersNames.Length)) return result;

                    // Save function params to process variables
                    Dictionary<string, Variable> tempVariablesStorage = new Dictionary<string, Variable>();
                    int i = 0;
                    foreach(string parameterName in ParametersNames)
                    {
                        if (Process.Variables.ContainsKey(parameterName)) 
                        {
                            tempVariablesStorage[parameterName] = Process.Variables[parameterName];
                        }
                        Process.Variables[parameterName] = _parameters[i++];
                    }

                    // Evaluate function expression
                    result = new Expression(Process, Expression).Evaluate(out status);
                    status.ThrowIfError();

                    // Restore process variables
                    foreach(KeyValuePair<string, Variable> kvp in tempVariablesStorage)
                    {
                        Process.Variables[kvp.Key] = kvp.Value;
                    }
                }
                else 
                {
                    if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Expression))
                    {
                        Process.Functions[Name] = this;
                        result = Process.Functions[Name].Call(out status);
                    }
                    else
                    {
                        throw new Exception($"Invalid function name {Name}");
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
                throw new Exception($"Invalid arguments for function {Name}");
            }
            return true;
        }

        public override string ToString()
{
            string paramsNames = "";
            if (ParametersNames != null)
            {
                foreach (string paramName in ParametersNames)
                {
                    paramsNames += paramName + ",";
                }

                if (paramsNames?.Length > 0) paramsNames = paramsNames.Substring(0, paramsNames.Length - 1);
            }

            return $"{Name}({paramsNames}) = {Expression}";
        }
    }
}
