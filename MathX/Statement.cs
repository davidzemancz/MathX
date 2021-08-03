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

        public Statement(string statement)
        {
            _statement = statement;
        }

        public Statement(string statement, Process process) : this(statement)
        {
            this._process = process;
        }

        public void Execute(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            try
            {
                if (_statement.Length > 0)
                {
                    string output = null;
                    bool complete = false;

                    // Looking for key chars
                    for (int i = 0; i < _statement.Length; i++)
                    {
                        char c = _statement[i];
                        if (c == '=') // Assignment to variable
                        {
                            string variableName = _statement.Substring(0, i);
                            string varibaleValueExpr = _statement.Substring(i + 1);
                            object variableValue = new Expression(varibaleValueExpr, _process).Evaluate(out status);

                            _process.Variables[variableName] = new Variable(variableName, variableValue);
                            output = $"{variableName} = {_process.Variables[variableName].Value}";
                            
                            complete = true;
                        }
                    }

                    // If si not any type of statement -> maybe its just expression 
                    if (!complete) 
                    {
                        string expression = _statement;
                        output = $"= {new Expression(expression, _process).Evaluate(out status).ToString()}";
                       
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
}
