using System.Collections.Generic;

namespace MathX.Processes
{
    public static class ProcessManager
    {
        public static Dictionary<string, Process> Processes { get; set; } = new Dictionary<string, Process>();
    }
}
