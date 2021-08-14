using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Forms;
using Base.Api;
using Base.UI.Api.Controls;
using MathX.Primitives;
using MathX.Processes;

namespace MathX.UI.Forms
{
    public partial class FormMathXMain : BaseForm
    {
        #region PROPS

        

        #endregion

        #region CONSTRUCTORS

        public FormMathXMain()
        {
            InitializeComponent();
         
        }

        #endregion

        #region PRIVATE METHODS

        #region Actions

        private void ShowGraphForm()
        {
            var form = new FormMathXGraph();
            form.Show();
        }

        private void ShowConsoleForm()
        {
            var form = new FormMathXConsole();
            form.Show();
        }

        private void ShowScriptEditorForm(bool shwoOpenFileDialog)
        {
            var form = new FormMathXScriptEditor();
            form.FormInput = new FormMathXScriptEditor.Input()
            {
                ShowOpenFileDialog = shwoOpenFileDialog,
            };
            form.Show();
        }

        private void LoadProcesses()
        {
            lbProcesses.Items.Clear();
            foreach (KeyValuePair<string, Process> kvp in ProcessManager.Processes)
            {
                lbProcesses.Items.Add(kvp.Value);
            }
            lbProcesses.SelectedIndex = 0;
        }

        #endregion

        #region Form

        private void FormMathXMain_Activated(object sender, System.EventArgs e)
        {
            LoadProcesses();
        }

        private void FormMathXMain_Load(object sender, System.EventArgs e)
        {
            Process defaultProcess = new Process("1");
            ProcessManager.Processes.Add(defaultProcess.Id, defaultProcess);
        }

        private void lbProcesses_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Process selectedProcess = (Process)lbProcesses.SelectedItem;

            lbVariables.Items.Clear();
            foreach (KeyValuePair<string, Variable> kvp in selectedProcess.Variables)
            {
                lbVariables.Items.Add(kvp.Value);
            }
            if (lbVariables.Items.Count < 1) lbVariables.Items.Add("[None]");

            lbFunctions.Items.Clear();
            foreach (KeyValuePair<string, Function> kvp in selectedProcess.Functions)
            {
                lbFunctions.Items.Add(kvp.Value);
            }
            if (lbFunctions.Items.Count < 1) lbFunctions.Items.Add("[None]");
        }

        private void menuStrip_ItemClicked(object sender, System.EventArgs e)
        {
            if(sender == consoleToolStripMenuItem)
            {
                this.ShowConsoleForm();
            }
        }

        private void shortcutButton_Click(object sender, System.EventArgs e)
        {
            if (sender == btnShortcutConsole)
            {
                this.ShowConsoleForm();
            }
            else if (sender == btnShortcutNewScript)
            {
                this.ShowScriptEditorForm(false);
            }
            else if (sender == btnShortcutOpenScript)
            {
                this.ShowScriptEditorForm(true);
            }
            else if (sender == btnShortcutLastScript)
            {
                this.ShowScriptEditorForm(false);
            }
            else if (sender == btnGraph)
            {
                this.ShowGraphForm();
            }
        }




        #endregion

        #endregion

        
    }
}
