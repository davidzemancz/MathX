using Base.Api;
using Base.UI.Api.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathX.Processes;
using System.IO;

namespace MathX.UI
{
    public partial class FormMathXConsole : BaseForm
    {
        #region CONSTS

        private const string _txtCommandLinePlaceholder = "Type here...";

        #endregion

        #region PROPS

        private Process _currentProcess;

        #endregion

        #region CONSTRUCTORS
        public FormMathXConsole()
        {
            InitializeComponent();
            
            txtCommandLine.GotFocus += txtCommandLine_GotFocus;
            txtCommandLine.LostFocus += txtCommandLine_LostFocus;

        }

        #endregion

        #region PRIVATE METHODS

        #region Actions

        private void LoadProcesses()
        {
            cbxProcesses.Items.Clear();
            foreach (KeyValuePair<string, Process> kvp in ProcessManager.Processes)
            {
                cbxProcesses.Items.Add(kvp.Value);
            }
            if (cbxProcesses.Items.Count > 0) cbxProcesses.SelectedIndex = 0;
        }

        #endregion

        #region Form

        private void FormMathXConsole_Activated(object sender, EventArgs e)
        {
            LoadProcesses();

        }

        private void FormMathXConsole_Load(object sender, EventArgs e)
        {
            txtCommandLine.Text = _txtCommandLinePlaceholder;
        }

        private void txtCommandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BaseStatus status;
                string statement = txtCommandLine.Text;

                _currentProcess.PushInput(statement);
                _currentProcess.Run(out status);
                
                using (StreamReader outputReader = new StreamReader(_currentProcess.Output, null, true, -1, true))
                {
                    if (status.State == BaseStatus.StateEnum.Ok)
                    {
                        txtOutput.Text += $">   {statement}\n";
                        txtOutput.Text += $"       {outputReader.ReadToEnd()}";
                    }
                    else
                    {
                        txtOutput.Text += $">   {status.Text}\n";
                    }
                }

               

                txtCommandLine.Text = "";
            }
        }


        private void txtCommandLine_GotFocus(object sender, EventArgs e)
        {
            if (txtCommandLine.Text == _txtCommandLinePlaceholder)
            {
                txtCommandLine.Text = "";
            }
        }

        private void txtCommandLine_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCommandLine.Text))
            {
                txtCommandLine.Text = _txtCommandLinePlaceholder;
            }
        }

        private void cbxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentProcess = cbxProcesses.SelectedItem as Process;
        }



        #endregion

        #endregion

       
    }
}
