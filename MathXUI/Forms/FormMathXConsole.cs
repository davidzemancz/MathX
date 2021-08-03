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

namespace MathX.UI
{
    public partial class FormMathXConsole : BaseForm
    {
        #region CONSTS

        private const string _txtCommandLinePlaceholder = "Type here...";

        #endregion

        #region PROPS

        private Process _consoleProcess;

        #endregion

        #region CONSTRUCTORS
        public FormMathXConsole()
        {
            InitializeComponent();
            _consoleProcess = new Process();
            _consoleProcess.Start();
            
            txtCommandLine.GotFocus += txtCommandLine_GotFocus;
            txtCommandLine.LostFocus += txtCommandLine_LostFocus;

        }

        #endregion

        #region PRIVATE METHODS


        #region Form

        private void FormMathXConsole_Load(object sender, EventArgs e)
        {
            txtCommandLine.Text = _txtCommandLinePlaceholder;
        }

        private void txtCommandLine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string statement = txtCommandLine.Text;
                _consoleProcess.ExecuteStatement(statement, out BaseStatus status);

                if (status.State == BaseStatus.StateEnum.Ok)
                {
                    txtCommandLineLog.Text += $">   {statement}\n";
                    txtCommandLineLog.Text += $"        {_consoleProcess.OutputReader.ReadToEnd()}\n";
                }
                else
                {
                    txtCommandLineLog.Text += $">   {status.Text}\n";
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

    #endregion

    #endregion


        }
}
