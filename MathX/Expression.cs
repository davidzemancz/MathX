using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Api;
using MathX.Processes;

namespace MathX
{
    public class Expression
    {
        private string _expression;
        private int _position;
        private Process _process;

       
        public Expression(Process process, string expression)
        {
            _expression = expression;
            _position = -1;
            this._process = process;
        }

        public Variable Evaluate(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            Variable result = null;
            try
            {
                result = Evaluate();
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Invalid expression] {ex.Message}", ex);
            }
            return result;
        }

        private Variable Evaluate(int operatorPriority = 0)
        {
            Variable result = new Variable(Variable.DataTypeEnum.None, "_");
            while ((++_position) < _expression.Length)
            {
                char expChar = _expression[_position];
                if (expChar == '$')
                {
                    string functionName = ReadFuncitonName();
                    List<Variable> parameters = new List<Variable>();
                    ++_position; // (
                    while (_expression[_position] != ')')
                    {
                        Variable parameter = Evaluate();
                        parameters.Add(parameter);
                    }
                    var function = new Function(functionName, parameters.ToArray());
                    result = function.Call();
                }
                else if (expChar == ';')
                {
                    return result;
                }
                else if (
                    char.IsLetter(expChar)
                    || expChar == '_')
                {
                    string variableName = ReadVariableName();
                    result = GetVariable(variableName);
                }
                else if (
                    char.IsDigit(expChar)
                    || (expChar == '-' && _position == 0)
                    || (expChar == '-' && _expression[_position - 1] == '('))
                {
                    result = ReadNumber();
                    result.DataType = Variable.DataTypeEnum.Double;
                }
                else if (expChar == '(')
                {
                    return Evaluate();
                }
                else if (expChar == ')')
                {
                    return result;
                }
                else // Is operator
                {
                    char expOperator = expChar;
                    int expOperatorPriority = GetPriority(expOperator);

                    if (expOperatorPriority < operatorPriority)
                    {
                        _position--;
                        return result;
                    }
                    else
                    {
                        Variable expResult = Evaluate(expOperatorPriority);

                        if (expOperator == '+')
                        {
                            result = Operation.Add(result, expResult);
                        }
                        else if (expOperator == '-')
                        {
                            result = Operation.Subtract(result, expResult);
                        }
                        else if (expOperator == '*')
                        {
                            result = Operation.Multiply(result, expResult);
                        }
                        else if (expOperator == '/')
                        {
                            result = Operation.Divide(result, expResult);
                        }
                        else if (expOperator == '^')
                        {
                            result = Operation.Power(result, expResult);
                        }
                    }
                }
            }

            if (result == null) throw new Exception("Expression is null");
            else if (result.DataType == Variable.DataTypeEnum.None) throw new Exception("Expression datatype is None");
            return result;
        }

        private string ReadFuncitonName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_expression[_position]);
            while (_position + 1 < _expression.Length && char.IsLetter(_expression[_position + 1]))
            {
                _position++;
                sb.Append(_expression[_position]);
            }
            return sb.ToString();
        }

        private string ReadVariableName()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_expression[_position]);
            while (_position + 1 < _expression.Length && char.IsLetter(_expression[_position + 1]))
            {
                _position++;
                sb.Append(_expression[_position]);
            }
            return sb.ToString();
        }

        private double ReadNumber()
        {
            bool minus = _expression[_position] == '-';
            double result = minus ? 0 : char.GetNumericValue(_expression[_position]); ;
            while (_position + 1 < _expression.Length && char.IsNumber(_expression[_position + 1]))
            {
                _position++;
                double num = char.GetNumericValue(_expression[_position]);
                result = 10 * result + num;
            }
            if (minus) result = result * -1;
            return result;
        }

        private Variable GetVariable(string variableName)
        {
            if (!_process.Variables.ContainsKey(variableName)) throw new Exception($"Undefined variable {variableName}");
            return _process.Variables[variableName];
        }

        private int GetPriority(char expOperator)
        {
            switch (expOperator)
            {
                case '+': return 1;
                case '-': return 2;
                case '*': return 3;
                case '/': return 4;
                case '^': return 5;
            }
            return 0;
        }

    }
}
