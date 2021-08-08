using Base.Api;
using MathX.Primitives;
using MathX.Primitives.Utils;
using MathX.Processes;
using MathX.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathX
{
    public class Interpreter
    {
        private string _fileName;
        private Process _process;

        public Interpreter(Process process, string fileName)
        {
            _process = process;
            _fileName = fileName;
        }

        public void Run(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            try
            {
                Dictionary<string, Label> labels = new Dictionary<string, Label>();
                Stack<Condition> conditions = new Stack<Condition>();
                Stack<Loop> loops = new Stack<Loop>();

                using (StreamReader streamreader = new StreamReader(_fileName))
                {
                    long linePositionEnd = 0;
                    while (!streamreader.EndOfStream)
                    {
                        string line = streamreader.ReadLine();
                        linePositionEnd = streamreader.GetActualPosition();

                        // Get statement info
                        var statement = new Statement(_process, line);
                        StatementInfo statementInfo = statement.GetInfo(out status);
                        status.ThrowIfError();

                        // Resolve statement info
                        if (statementInfo.Label != null)
                        {
                            if (conditions.Count > 0) throw new Exception($"Label {statementInfo.Label.Name} could not be placed in condition");
                            
                            statementInfo.Label.Position = linePositionEnd;
                            labels[statementInfo.Label.Name] = statementInfo.Label;
                        }
                        else if (statementInfo.Loop != null)
                        {
                            statementInfo.Loop.Position = linePositionEnd;
                            statementInfo.Loop.EvaluateCondition(_process, out status);
                            status.ThrowIfError();

                            loops.Push(statementInfo.Loop);
                        }
                        else if (statementInfo.LoopEnd)
                        {
                            Loop loop = loops.Peek();
                            loop.EvaluateCondition(_process, out status);
                            status.ThrowIfError();

                            if (loop.Result)
                            {
                                streamreader.BaseStream.Seek(loop.Position, SeekOrigin.Begin);
                                streamreader.DiscardBufferedData();
                            }
                            else
                            {
                                loops.Pop();
                            }
                        }
                        else if (statementInfo.Condition != null)
                        {
                            statementInfo.Condition.Evaluate(_process, out status);
                            status.ThrowIfError();

                            conditions.Push(statementInfo.Condition);
                        }
                        else if (statementInfo.ConditionEnd)
                        {
                            conditions.Pop();
                        }
                        else
                        {
                            // All conditions are met
                            if ((conditions.Count == 0 || conditions.All(c => c.Result)) && (loops.Count == 0 || loops.All(l => l.Result)))
                            {
                                // Execute statement
                                statement.Execute(statementInfo, out status);

                                // Resolve statemant status & info 
                                if (status.State == BaseStatus.StateEnum.Error) // Error
                                {
                                    throw new Exception(status.Text, status.Exception);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Runtime exception] {ex.Message}", ex);
            }
        }
    }
}
