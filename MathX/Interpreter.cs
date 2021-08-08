using Base.Api;
using MathX.Primitives;
using MathX.Processes;
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
                Stack<Block> blocks = new Stack<Block>();

                using (StreamReader fileReader = new StreamReader(_fileName))
                {
                    while (!fileReader.EndOfStream)
                    {
                        long lineStartPosition = fileReader.BaseStream.Position;
                        string line = fileReader.ReadLine();

                        var statement = new Statement(_process, line);
                        statement.Execute(out StatementInfo statementInfo, out status);
                        if (status.State == BaseStatus.StateEnum.Error)
                        {
                            throw new Exception(status.Text, status.Exception);
                        }
                        else if (statementInfo.IsLabel)
                        {
                            labels[statementInfo.LabelName] = new Label(lineStartPosition, statementInfo.LabelName);
                        }
                        else if (statementInfo.BlockStart)
                        {
                            blocks.Push(new Block());
                        }
                        else if (statementInfo.BlockEnd)
                        {
                            var block = blocks.Peek();
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
