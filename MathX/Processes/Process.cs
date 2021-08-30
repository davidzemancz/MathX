using System;
using System.Collections.Generic;
using System.IO;
using Base.Api;
using MathX.Primitives;
using MathX.Primitives.Utils;

namespace MathX.Processes
{
    public class Process : IDisposable
    {
        public string Id { get; set; }
        public StateEnum State { get; set; }
        public BaseDictionary<string, Variable> Variables { get; set; }
        public BaseDictionary<string, Function> Functions { get; set; }
        private MemoryStream Output { get; set; }
        public StreamWriter OutputWriter { get; set; }
        public StreamReader OutputReader { get; protected set; }

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
            OutputWriter = new StreamWriter(Output);
            OutputReader = new StreamReader(Output);
            State = StateEnum.Running;
        }
        
        public void ExecuteStatement(string line, out BaseStatus status)
        {
            Statement statement = new Statement(this, line);
            StatementInfo statementInfo = statement.GetInfo(out status);
            if (status.State == BaseStatus.StateEnum.Ok) 
            {
                statement.Execute(statementInfo, out status);
            }
        }

        public void Dispose()
        {
            this.Output.Dispose();
            this.OutputReader.Dispose();
            this.OutputWriter.Dispose();
        }

        public override string ToString()
        {
            return $"Process {Id} [State {State}]";
        }
    }
}
