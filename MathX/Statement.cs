using Base.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MathX.Processes;

namespace MathX
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

        public void Execute(out StatementInfo info, out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            info = new StatementInfo();
            try
            {
                if (_statement.Length > 0)
                {
                    string output = null;
                    bool complete = false;

                    // Looking for keywords
                    if (_statement.StartsWith("if")) // Condition
                    {
                        bool result = false;

                        string conditonExpression = _statement.Substring(3);
                        Variable variable = new Expression(_process, conditonExpression).Evaluate(out status);
                        if (variable.DataType == Variable.DataTypeEnum.Double) 
                        {
                            result = (double)variable.Value > 0;
                        }

                        info.IsCondition = true;
                        info.ConditionResult = result;

                        output = $"= {result.ToString()}";
                        complete = true;
                    }
                    else if (_statement.StartsWith("$")) // Function
                    {
                        complete = true;
                    }

                    // If no keywords at beginning found
                    if (!complete)
                    {
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
                            else if (c == '#') // Label
                            {
                                string labelName = _statement.Substring(i + 1);
                                info.IsLabel = true;
                                info.LabelName = labelName;

                                complete = true;
                                break;
                            }
                        }
                    }

                    // If still not recognized -> maybe its just expression 
                    if (!complete) 
                    {
                        string expression = _statement;
                        output = $"= {new Expression(_process, expression).Evaluate(out status)?.Value}";
                        complete = true;
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

    public class StatementInfo
    {
        public bool IsLabel { get; set; }
        public string LabelName { get; set; }
        public bool IsCondition { get; set; }
        public bool ConditionResult { get; set; }
        public bool BlockStart { get; set; }
        public bool BlockEnd { get; set; }
    }
}
