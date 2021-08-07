using Base.Api;
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
                using (StreamReader fileReader = new StreamReader(_fileName))
                {
                    while (!fileReader.EndOfStream)
                    {
                        string line = fileReader.ReadLine();
                        _process.ExecuteStatement(line, out status);
                        if (status.State == BaseStatus.StateEnum.Error) throw new Exception(status.Text, status.Exception);
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
