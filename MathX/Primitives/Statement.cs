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
                    if (_statement.StartsWith("if ")) // Condition
                    {
                        statementInfo.ConditionStart = true;
                        string conditonExpression = _statement.Substring(3);
                        statementInfo.Condition = new Condition(conditonExpression);
                    }
                    else if(_statement.StartsWith("while ")) // Loop
                    {
                        statementInfo.LoopStart = true;
                        string loopExpression = _statement.Substring(6);
                        statementInfo.Loop = new Loop(-1, loopExpression);
                    }
                    else if (_statement.StartsWith("end")) // Block end
                    {
                        if (_statement == "endif ")
                        {
                            statementInfo.ConditionEnd = true;
                        }
                        else if (_statement == "endwhile ")
                        {
                            statementInfo.LoopEnd = true;
                        }
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
                    if (statementInfo.Condition != null) // Condition
                    {
                        bool result = statementInfo.Condition.Result;
                        output = $"= {result}";
                    }
                    else if (statementInfo.ConditionEnd) // Condition end
                    {
                       // ignore
                    }
                    else if (statementInfo.Label != null)
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
                            if (c == '=') // Assignment to variable
                            {
                                string variableName = _statement.Substring(0, i);
                                string varibaleValueExpr = _statement.Substring(i + 1);
                                Variable variable = new Expression(_process, varibaleValueExpr).Evaluate(out status);
                                if (variable != null)
                                {
                                    variable.Name = variableName;
                                    _process.Variables[variableName] = variable;
                                    output = $"{variableName} = {_process.Variables[variableName].Value}";

                                }
                                complete = true;
                                break;
                            }
                        }

                        // If still not recognized -> maybe its just expression 
                        if (!complete)
                        {
                            string expression = _statement;
                            output = $"= {new Expression(_process, expression).Evaluate(out status)?.Value}";
                            complete = true;
                        }
                    }
                    _process.WriteToOutput(output);
                }
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Invalid statement] {ex.Message}", ex);
            }
        }
    }

}
