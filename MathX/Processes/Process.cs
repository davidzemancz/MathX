using System;
using System.Collections.Generic;
using System.IO;
using Base.Api;

namespace MathX.Processes
{
    public class Process
    {
        public Dictionary<string, Variable> Variables { get; set; }

        public bool Running { get; private set; }

        protected MemoryStream Output { get; set; }

        protected StreamWriter OutputWriter { get; set; }

        public StreamReader OutputReader { get; protected set; }

        public Process()
        {
            Variables = new Dictionary<string, Variable>();
            Output = new MemoryStream();
            OutputWriter = new StreamWriter(Output);
            OutputReader = new StreamReader(Output);
        }
        
        public void Start()
        {
            Running = true;
        }
      
        public void Stop()
        {
            if (!Running) throw new Exception("Process is not running");
            Running = false;
        }

        public void ExecuteStatement(string statement, out BaseStatus status)
        {
            new Statement(statement, this).Execute(out status);
        }

        public void WriteToOutput(string data)
        {
            if (!Running) throw new Exception("Process is not running");
            else if (string.IsNullOrEmpty(data)) return;

            long position = Output.Position;
            OutputWriter.Write(data);
            OutputWriter.Flush();
            Output.Seek(position, SeekOrigin.Begin);
        }

    }
}
