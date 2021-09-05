using System.Collections.Generic;
using Base.Api;

namespace MathX.Processes
{
    public static class ProcessManager
    {
        public static BaseDictionary<string, Process> Processes { get; set; } = new BaseDictionary<string, Process>();
    }
}
