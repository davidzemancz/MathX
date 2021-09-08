using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Api;
using MathX.Datatypes;
using MathX.Processes;

namespace MathX.Primitives
{
    public class Expression
    {
        #region FIELDS

        private string _expression;
        private int _position;
        private Process _process;

        #endregion

        #region CONSTURCOTRS

        public Expression(Process process, string expression)
        {
            _expression = expression;
            _position = -1; // ++_position in the begining of evaluation
            _process = process;
        }

        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Evaluate the expression
        /// </summary>
        /// <param name="status">Status of call</param>
        /// <returns>Result of expression</returns>
        public Variable Evaluate(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            Variable result = new Variable();
            try
            {
                _position = -1;
                result = Evaluate();
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Invalid expression] {ex.Message}", ex);
            }
            return result;
        }

        #endregion

        #region PRIVATE METHODS

        private Variable Evaluate(int operatorPriority = 0)
        {
            Variable result = new Variable(Variable.DataTypeEnum.None, "_");
            while (++_position < _expression?.Length)
            {
                char expChar = _expression[_position];
                if (expChar == ',') // Function param delimiter
                {
                    _position--;
                    return result;
                }
                else if (char.IsLetter(expChar)|| expChar == '_') // Variable or function
                {
                    string name = ReadName(out bool isFunction, out bool hasIndexer);

                    if (isFunction)
                    {
                        result = CallFuntion(name);
                    }
                    else
                    {
                        result = GetVariable(name);
                    }

                    if (hasIndexer)
                    {
                        if (result.DataType == Variable.DataTypeEnum.Vector)
                        {
                            Vector vector = (Vector)result.Value;
                            if (vector != null)
                            {
                                List<Variable> parameters = GetParameters(true, ']');
                                int index = int.Parse(parameters[0].Value?.ToString() ?? "-1");
                                if (index < 0 || index >= vector.Dimension) // Out of vector range
                                {
                                    throw new IndexOutOfRangeException("Index is out of vector range");
                                }

                                result = vector[index];
                            }
                            else throw new Exception($"Null variable {name}");
                        }
                        else throw new Exception("Datatype does not support indexing");
                    }
                }
                else if (char.IsDigit(expChar)) // Number
                {
                    result = ReadNumber();
                }
                else if (expChar == '[') // Vector or Matrix
                {   
                    result = ReadVector();
                }
                else if (expChar == ']') // End of vector or indexer
                {
                    return result;
                }
                else if (expChar == '(') // Start of parentheses block
                {
                    return Evaluate();
                }
                else if (expChar == ')') // End of parentheses block
                {
                    return result;
                }
                else if (IsOperator(expChar)) // Operator
                {
                    char expOperator = expChar;
                    int expOperatorPriority = GetPriority(expOperator);

                    // Operator with lower priority AND NOT plus or minus before signed number
                    if (expOperatorPriority <= operatorPriority && !(_position > 0 && IsOperator(_expression[_position - 1])))
                    {
                        _position--;
                        return result;
                    }
                    else
                    {
                        //operatorPriority = expOperatorPriority;

                        if (expOperator == '+')
                        {
                            result = result + Evaluate(expOperatorPriority);
                        }
                        else if (expOperator == '-')
                        {
                            result = result - Evaluate(expOperatorPriority);
                        }
                        else if (expOperator == '*')
                        {
                            result = result * Evaluate(expOperatorPriority);
                           
                        }
                        else if (expOperator == '/')
                        {
                            result = result / Evaluate(expOperatorPriority);
                        }
                        else if (expOperator == '%')
                        {
                            result = result % Evaluate(expOperatorPriority);
                        }
                        else if (expOperator == '^')
                        {
                            result = new Function(_process, Function.Power, new[] { result, Evaluate(expOperatorPriority) }).Call(out BaseStatus status);
                            status.ThrowIfError();
                        }
                        else if (expOperator == '&')
                        {
                            result = result & Evaluate(expOperatorPriority);
                        }
                        else if (expOperator == '|')
                        {
                            result = result | Evaluate(expOperatorPriority);
                        }
                        else if (expOperator == '>')
                        {
                            if (_position + 1 < _expression.Length && _expression[_position + 1] == '=')
                            {
                                _position++;
                                result = result >= Evaluate(expOperatorPriority);
                            }
                            else
                            {
                                result = result > Evaluate(expOperatorPriority);
                            }
                        }
                        else if (expOperator == '<')
                        {
                            if (_position + 1 < _expression.Length && _expression[_position + 1] == '=')
                            {
                                _position++;
                                result = result <= Evaluate(expOperatorPriority);
                            }
                            else
                            {
                                result = result < Evaluate(expOperatorPriority);
                            }
                        }
                        else if (expOperator == '=')
                        {
                            if (_position + 1 < _expression.Length && _expression[_position + 1] == '=')
                            {
                                _position++;
                                result = result == Evaluate(expOperatorPriority);
                            }
                            else throw new Exception("Invalid expression operator");
                        }
                        else if (expOperator == '!')
                        {
                            if (_position + 1 < _expression.Length && _expression[_position + 1] == '=')
                            {
                                _position++;
                                result = result != Evaluate(expOperatorPriority);
                            }
                            else throw new Exception("Invalid expression operator");
                        }
                    }
                }
            }

            return result;
        }

        private List<Variable> GetParameters(bool skipOpeningChar, char closingChar)
        {
            
            List<Variable> parameters = new List<Variable>();

            if (skipOpeningChar) ++_position;
            while (_expression.Length > _position && _expression[_position] != closingChar)
            {
                if (_position + 1 < _expression.Length && _expression[_position + 1] == ',') _position++;
                Variable parameter = Evaluate();
                parameters.Add(parameter);
            }

            return parameters;
        }

        private Variable CallFuntion(string name)
        {
            Variable result;
           
            List<Variable> parameters = GetParameters(true,')');

            if (_process.Functions.ContainsKey(name)) // User defined function
            {
                result = _process.Functions[name].Call(parameters.ToArray(), out BaseStatus status);
                status.ThrowIfError();
            }
            else // Inbuilt function
            {
                var function = new Function(_process, name, parameters.ToArray());
                result = function.Call(out BaseStatus status);
                status.ThrowIfError();
            }
            return result;
        }

        private bool IsOperator(char oper)
        {
            char[] opers = new[] { '+','-','*','/','^','>','<','=','!','%','&','|' };
            return opers.Contains(oper);
        }

        private string ReadName(out bool isFunction, out bool hasIndexer)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(_expression[_position]);
            while (_position + 1 < _expression.Length && char.IsLetter(_expression[_position + 1]))
            {
                _position++;
                sb.Append(_expression[_position]);
            }

            isFunction = _expression.Length > (_position + 1) && _expression[_position + 1] == '(';
            hasIndexer = _expression.Length > (_position + 1) && _expression[_position + 1] == '[';

            return sb.ToString();
        }

        private Vector ReadVector()
        {
            List<Variable> components = GetParameters(false,']');
            return new Vector(components.ToArray());
        }

        private double ReadNumber()
        {
            bool minus = _expression[_position] == '-';
            bool isDecimal = false;
            int decimalDigits = 0;
            double result = minus ? 0 : char.GetNumericValue(_expression[_position]); ;
            while (_position + 1 < _expression.Length && (char.IsNumber(_expression[_position + 1]) || _expression[_position + 1] == '.'))
            {
                _position++;
                if (_expression[_position] == '.')
                {
                    isDecimal = true;
                }
                else
                {
                    if (isDecimal)
                    {
                        double num = char.GetNumericValue(_expression[_position]);
                        result = result + Math.Pow(10, -(++decimalDigits)) * num;
                    }
                    else
                    {
                        double num = char.GetNumericValue(_expression[_position]);
                        result = 10 * result + num;
                    }
                }
                
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
                case '%': return 6;
                case '|': return 7;
                case '&': return 8;
                case '<':
                case '>':
                case '=':
                case '!':
                    return 9;
                
               
            }
            return 0;
        }


        #endregion
    }
}
