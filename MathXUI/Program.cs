using System;
using System.Windows.Forms;
using MathX.Processes;
using MathX.UI.Forms;

namespace MathX.UI
{
    static class Program
    {
        public static ProcessManager ProcessManager { get; private set; }
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ProcessManager = new ProcessManager();

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(new FormMathXMain());
        }
    }
}
