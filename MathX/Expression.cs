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

        public Expression(string expression)
        {
            _expression = expression;
            _position = -1;
        }

        public Expression(string expression, Process process) : this(expression)
        {
            this._process = process;
        }

        public object Evaluate(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            object result = 0;
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

        private object Evaluate(int operatorPriority = 0)
        {
            object result = null;

            while ((++_position) < _expression.Length)
            {
                char expChar = _expression[_position];
                if (
                    char.IsLetter(expChar)
                    || expChar == '_')
                {
                    string variableName = ReadVariableName();
                    result = GetVariableValue(variableName);
                }
                else if (
                    char.IsDigit(expChar)
                    || (expChar == '-' && _position == 0)
                    || (expChar == '-' && _expression[_position - 1] == '('))
                {
                    result = ReadNumber();
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
                        object expResult = Evaluate(expOperatorPriority);

                        if (expOperator == '+')
                        {
                            result = Add(result, expResult);
                        }
                        else if (expOperator == '-')
                        {
                            result = Subtract(result, expResult);
                        }
                        else if (expOperator == '*')
                        {
                            result = Multiply(result, expResult);
                        }
                        else if (expOperator == '/')
                        {
                            result = Divide(result, expResult);
                        }
                        else if (expOperator == '^')
                        {
                            result = Caret(result, expResult);
                        }
                    }
                }
            }

            return result;
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

        private object GetVariableValue(string variableName)
        {
            if (!_process.Variables.ContainsKey(variableName)) throw new Exception($"Undefined variable {variableName}");
            return _process.Variables[variableName].Value;
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

        private object Add(object addend1, object addend2)
        {
            return (double)addend1 + (double)addend2;
        }

        private object Subtract(object minued, object subtrahend)
        {
            return (double)minued - (double)subtrahend;
        }

        private object Multiply(object factor1, object factor2)
        {
            return (double)factor1 * (double)factor2;
        }

        private object Divide(object dividend, object divisor)
        {
            return (double)dividend / (double)divisor;
        }
        
        private object Caret(object o1, object o2)
        {
            return MathX.Pwr((double)o1, (double)o2);
        }
    }
}
