using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Base.Api;
using MathX.Primitives;
using MathX.Primitives.Utils;
using MathX.Utils;

namespace MathX.Processes
{
    public class Process : IDisposable
    {

        private BaseDictionary<string, Function> _functions;

        public string Id { get; set; }
        public StateEnum State { get; set; }
        public BaseDictionary<string, Variable> Variables { get; set; }

        public BaseDictionary<string, Function> Functions
        {
            get => _functions;
            set
            {
                _functions = value;
                foreach (KeyValuePair<string, Function> kvp in _functions)
                {
                    kvp.Value.Process = this;
                }
                _functions.ItemAdded += key => { Functions[key].Process = this; };
            }
        }
        
        [JsonIgnore]
        public MemoryStream Output { get; set; }
        [JsonIgnore]
        public MemoryStream Input { get; set; }

        public enum StateEnum
        {
            Pending = 1,
            Running = 2,
            Stopped = 3
        }

        public Process()
        {
            Variables = new BaseDictionary<string, Variable>();
            Functions = new BaseDictionary<string, Function>();
            Output = new MemoryStream();
            Input = new MemoryStream();
            State = StateEnum.Pending;
        }

        public Process(string id) : this()
        {
            Id = id;
        }
        
        public void Run(out BaseStatus status)
        {
            State = StateEnum.Running;
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            try
            {
                Dictionary<string, Label> labels = new Dictionary<string, Label>();
                Stack<Condition> conditions = new Stack<Condition>();
                Stack<Loop> loops = new Stack<Loop>();

                using (StreamReader inputReader = new StreamReader(Input, null, true, -1, true))
                {
                    long linePositionEnd = 0;
                    while (!inputReader.EndOfStream)
                    {
                        string line = inputReader.ReadLine();
                        line = line.Trim();
                        if (string.IsNullOrEmpty(line)) continue;

                        linePositionEnd = inputReader.GetActualPosition();

                        // Get statement info
                        var statement = new Statement(this, line);
                        StatementInfo statementInfo = statement.GetInfo(out status);
                        status.ThrowIfError();

                        // Resolve statement info
                        if (statementInfo.Label != null)
                        {
                            if (conditions.Count > 0) throw new Exception($"Label {statementInfo.Label.Name} could not be placed in condition");
                            else if (loops.Count > 0) throw new Exception($"Label {statementInfo.Label.Name} could not be placed in loop");

                            statementInfo.Label.Position = linePositionEnd;
                            labels[statementInfo.Label.Name] = statementInfo.Label;
                        }
                        else if (statementInfo.Loop != null)
                        {
                            if ((conditions.Count == 0 || conditions.All(c => c.Result)))
                            {
                                if (statementInfo.Loop.Keyword == Keywords.While)
                                {
                                    statementInfo.Loop.Position = linePositionEnd;
                                    statementInfo.Loop.EvaluateCondition(this, out status);
                                    status.ThrowIfError();

                                    loops.Push(statementInfo.Loop);
                                }
                                else if (statementInfo.Loop.Keyword == Keywords.EndWhile)
                                {
                                    Loop loop = loops.Peek();
                                    loop.EvaluateCondition(this, out status);
                                    status.ThrowIfError();

                                    if (loop.Result)
                                    {
                                        inputReader.BaseStream.Seek(loop.Position, SeekOrigin.Begin);
                                        inputReader.DiscardBufferedData();
                                    }
                                    else
                                    {
                                        loops.Pop();
                                    }
                                }
                            }
                        }
                        else if (statementInfo.Condition != null)
                        {
                            if ((loops.Count == 0 || loops.All(l => l.Result)))
                            {
                                if (statementInfo.Condition.Keyword == Keywords.If)
                                {
                                    statementInfo.Condition.Evaluate(this, out status);
                                    status.ThrowIfError();

                                    conditions.Push(statementInfo.Condition);
                                }
                                else if (statementInfo.Condition.Keyword == Keywords.ElseIf)
                                {
                                    throw new NotImplementedException("ElseIf block is not implemented yet");
                                }
                                else if (statementInfo.Condition.Keyword == Keywords.Else)
                                {
                                    var currentCondition = conditions.Pop();
                                    statementInfo.Condition.Result = !currentCondition.Result;
                                    conditions.Push(statementInfo.Condition);
                                }
                                else if (statementInfo.Condition.Keyword == Keywords.EndIf)
                                {
                                    conditions.Pop();
                                }
                            }
                        }
                        else
                        {
                            // All conditions are met
                            if ((conditions.Count == 0 || conditions.All(c => c.Result)) && (loops.Count == 0 || loops.All(l => l.Result)))
                            {
                                // Execute statement
                                statement.Execute(statementInfo, out status);
                                status.ThrowIfError();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Runtime exception] {ex.Message}", ex);
            }
            finally
            {
                State = StateEnum.Pending;
            }
        }

        public void PushInput(string line)
        {
            if (line == null) return;

            long position = Input.Position;
            using (StreamWriter inputWriter = new StreamWriter(Input, null, -1, true))
            {
                // Write to end
                Output.Seek(Output.Length, SeekOrigin.Begin);
                inputWriter.WriteLine(line);
            }
            // Seek to prev position
            Input.Seek(position, SeekOrigin.Begin);
        }

        public void PushOutput(string line)
        {
            if (line == null) return;

            long position = Output.Position;
            using (StreamWriter outputWriter =  new StreamWriter(Output, null, -1, true))
            {
                // Write to end
                Output.Seek(0, SeekOrigin.End);
                outputWriter.WriteLine(line);
            }
            // Seek to prev position
            Output.Seek(position, SeekOrigin.Begin);
        }

        public void ClearInput()
        {
            Input.Dispose();
            Input = new MemoryStream();
        }

        public void ClearOutput()
        {
            Output.Dispose();
            Output = new MemoryStream();
        }

        public void Dispose()
        {
            Output.Dispose();
            Input.Dispose();
        }

        public override string ToString()
        {
            return $"Process {Id} [State {State}]";
        }
    }
}
