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
        public string Id { get; set; }
        public StateEnum State { get; set; }
        public BaseDictionary<string, Variable> Variables { get; set; }
        public BaseDictionary<string, Function> Functions { get; set; }
        [JsonIgnore]
        public MemoryStream Output { get; set; }
        [JsonIgnore]
        public StreamWriter OutputWriter { get; set; }
        [JsonIgnore]
        public StreamReader OutputReader { get; protected set; }
        [JsonIgnore]
        public MemoryStream Input { get; set; }
        [JsonIgnore]
        public StreamWriter InputWriter { get; set; }
        [JsonIgnore]
        public StreamReader InputReader { get; protected set; }

        public enum StateEnum
        {
            Running = 1,
            Stopped = 2,
            Terminated = 3
        }

        public Process(string id)
        {
            Id = id;
            Variables = new BaseDictionary<string, Variable>();
            Functions = new BaseDictionary<string, Function>();
            Output = new MemoryStream();
            OutputReader = new StreamReader(Output);
            Input = new MemoryStream();
            InputWriter = new StreamWriter(Input);
            InputReader = new StreamReader(Input);
            State = StateEnum.Running;
        }
        
        public void Run(out BaseStatus status)
        {
            status = new BaseStatus(BaseStatus.StateEnum.Ok, "");
            try
            {
                Dictionary<string, Label> labels = new Dictionary<string, Label>();
                Stack<Condition> conditions = new Stack<Condition>();
                Stack<Loop> loops = new Stack<Loop>();

                long linePositionEnd = 0;
                while (!InputReader.EndOfStream)
                {
                    string line = InputReader.ReadLine();
                    line = line.Trim();
                    if (string.IsNullOrEmpty(line)) continue;

                    linePositionEnd = InputReader.GetActualPosition();

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
                                InputReader.BaseStream.Seek(loop.Position, SeekOrigin.Begin);
                                InputReader.DiscardBufferedData();
                            }
                            else
                            {
                                loops.Pop();
                            }
                        }
                    }
                    else if (statementInfo.Condition != null)
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
            catch (Exception ex)
            {
                status = new BaseStatus(BaseStatus.StateEnum.Error, $"[Runtime exception] {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            Output.Dispose();
            OutputReader.Dispose();
            OutputWriter.Dispose();
            Input.Dispose();
            InputWriter.Dispose();
            InputReader.Dispose();
        }

        public override string ToString()
        {
            return $"Process {Id} [State {State}]";
        }
    }
}
