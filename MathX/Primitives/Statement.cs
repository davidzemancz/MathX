using Base.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathX.Processes;
using MathX.Primitives.Utils;
using System.Numerics;
using Vector = MathX.Datatypes.Vector;

namespace MathX.Primitives
{
    public class Statement
    {
        private string _statement;
        private Process _process;

        public Statement(Process process, string statement)
        {
            _statement = statement;
            _process = process;
        }

        public StatementInfo GetInfo(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            StatementInfo statementInfo = new StatementInfo();
            try
            {
                if (_statement.Length > 0)
                {
                    if (_statement.StartsWith("if ")) 
                    {
                        string conditonExpression = _statement.Substring(3);
                        statementInfo.Condition = new Condition(Keywords.If, conditonExpression);
                    }
                    if (_statement.StartsWith("elseif "))
                    {
                        string conditonExpression = _statement.Substring(3);
                        statementInfo.Condition = new Condition(Keywords.ElseIf, conditonExpression);
                    }
                    if (_statement == "else") 
                    {
                        string conditonExpression = _statement.Substring(3);
                        statementInfo.Condition = new Condition(Keywords.Else, conditonExpression);
                    }
                    if (_statement == "endif") 
                    {
                        statementInfo.Condition = new Condition(Keywords.EndIf, null);
                    }
                    else if(_statement.StartsWith("while ")) 
                    {
                        string loopExpression = _statement.Substring(6);
                        statementInfo.Loop = new Loop(-1, Keywords.While, loopExpression);
                    }
                    else if (_statement == "endwhile")
                    {
                        statementInfo.Loop = new Loop(-1, Keywords.EndWhile, null);
                    }

                    else if(_statement.StartsWith("#")) // Label
                    {
                        statementInfo.Label = new Label(-1, _statement.Substring(1));
                    }
                }
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Statement info error] {ex.Message}", ex);
            }
            return statementInfo;
        }

        public void Execute(StatementInfo statementInfo, out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            try
            {
                if (_statement.Length > 0)
                {
                    string output = null;
                   
                    // Looking for keywords
                    if (statementInfo?.Condition != null) // Condition
                    {
                        bool result = statementInfo.Condition.Result;
                        output = $"= {result}";
                    }
                    else if (statementInfo?.Label != null)
                    {
                        string labelName = statementInfo.Label.Name;
                        output = $"LABEL {labelName}";
                    }
                    else
                    {
                        bool complete = false;

                        // Looking for key chars
                        for (int i = 0; i < _statement.Length; i++)
                        {
                            char c = _statement[i];
                            if (c == '=') // Assignment to variable or inline function definition
                            {
                                string name = _statement.Substring(0, i);
                                if (name.Contains('(')) // Function
                                {
                                    string[] arr = name.Split('(');
                                    string functionName = arr[0];
                                    string[] functionParamsNames = arr[1].Substring(0, arr[1].Length - 1).Split(',');

                                    string expression = _statement.Substring(i + 1);

                                    Function function = new Function(_process, functionName, expression, functionParamsNames);
                                    _process.Functions[functionName] = function;

                                    output = $"{name} = {expression}";
                                }
                                else if (name.Contains('[')) // Indexer of variable (vector or matrix)
                                {
                                    string[] arr = name.Split('[');
                                    string variableName = arr[0];
                                    string[] indexes = arr[1].Substring(0, arr[1].Length - 1).Split(',');

                                    string expression = _statement.Substring(i + 1);

                                    if (_process.Variables.ContainsKey(variableName))
                                    {
                                        Variable variable = _process.Variables[variableName];
                                        if (variable.DataType == Variable.DataTypeEnum.Vector)
                                        {
                                            Vector vector = (Vector)variable.Value;
                                            if (vector != null)
                                            {
                                                Variable indexVariable = new Expression(_process, indexes[0]).Evaluate(out status);
                                                int index = int.Parse(indexVariable.Value?.ToString() ?? "-1");
                                                if (index < 0 || index >= vector.Dimension) // Out of vector range
                                                {
                                                    throw new IndexOutOfRangeException("Index is out of vector range");
                                                }
                                                
                                                Variable result = new Expression(_process, expression).Evaluate(out status);
                                                vector[index] = result;
                                                output = $"{variable}";
                                            }
                                            else throw new Exception($"Null variable {variableName}");
                                        }
                                        else throw new Exception("Datatype does not support indexing");
                                    }
                                    else throw new Exception($"Undefined variable {variableName}");
                                }
                                else // Variable
                                {
                                    string varibaleValueExpr = _statement.Substring(i + 1);
                                    Variable result = new Expression(_process, varibaleValueExpr).Evaluate(out status);
                                    if (result.Value != null)
                                    {
                                        result.Name = name;
                                        _process.Variables[name] = result;
                                        output = $"{result}";

                                    }
                                }
                                complete = true;
                                break;
                            }
                        }

                        // If still not recognized -> maybe its just expression 
                        if (!complete)
                        {
                            string expressionStr = _statement;
                            Expression expression = new Expression(_process, expressionStr);
                            Variable result = expression.Evaluate(out status);
                            if (result.Value != null) {
                                output = $"{expressionStr} = {result.Value}";
                            }
                            complete = true;
                        }
                    }
                    _process.PushOutput(output);
                }
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Invalid statement] {ex.Message}", ex);
            }
        }
    }

}
