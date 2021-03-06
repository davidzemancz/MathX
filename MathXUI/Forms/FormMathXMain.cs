using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Base.Api;
using Base.UI.Api.Controls;
using Base.UI.Api.Utils;
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
            FileHandler = new BaseFileHandler(this, Settings.Encoding, "Json files (*.json)|*.json|All files (*.*)|*.*");
        }

        #endregion

        #region PRIVATE METHODS

        #region Actions

        private byte[] GetStateData()
        {
            return JsonSerializer.SerializeToUtf8Bytes(ProcessManager.Processes);
        }

        private void LoadStateData(byte[] data)
        {
            if (data == null) // New file
            {
                ProcessManager.Processes.Clear();
                Process defaultProcess = new Process("1");
                ProcessManager.Processes.Add(defaultProcess.Id, defaultProcess);
                FileHandler.UnsavedChanges = false;
            }
            else // Open file
            {
                ProcessManager.Processes = JsonSerializer.Deserialize<BaseDictionary<string, Process>>(data);
                FileHandler.UnsavedChanges = false;
            }

            LoadProcesses();
        }

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

        private void ShowScriptEditorForm(bool showOpenFileDialog, string fileName = "")
        {
            var form = new FormMathXScriptEditor();
            form.FormInput = new FormMathXScriptEditor.Input()
            {
                ShowOpenFileDialog = showOpenFileDialog,
                FileName = fileName
            };
            form.Show();
        }

        private void LoadProcesses()
        {
            lbProcesses.Items.Clear();
            foreach (KeyValuePair<string, Process> kvp in ProcessManager.Processes)
            {
                lbProcesses.Items.Add(kvp.Value);

                kvp.Value.Variables.ItemAdded -= UnsavedChanges;
                kvp.Value.Variables.ItemAdded += UnsavedChanges;

                kvp.Value.Functions.ItemAdded -= UnsavedChanges;
                kvp.Value.Functions.ItemAdded += UnsavedChanges;
            }
            if (lbProcesses.Items.Count > 0) lbProcesses.SelectedIndex = 0;

            ProcessManager.Processes.ItemAdded -= UnsavedChanges;
            ProcessManager.Processes.ItemAdded += UnsavedChanges;
        }

        private void UnsavedChanges(string key)
        {
            FileHandler.UnsavedChanges = true;
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

        private void FormMathXMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                byte[] data = this.FileHandler.NewFile(GetStateData());
                LoadStateData(data);
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                byte[] data = this.FileHandler.OpenFile(GetStateData());
                LoadStateData(data);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                FileHandler.SaveFile(GetStateData());
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                FileHandler.SaveFileAs(GetStateData());
            }
        }

        private void FormMathXMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileHandler.CheckUnsavedChanges(GetStateData(), out bool cancel);
            e.Cancel = cancel;
        }

        private void lbProcesses_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Process selectedProcess = (Process)lbProcesses.SelectedItem;

            lbVariables.Items.Clear();
            foreach (KeyValuePair<string, Variable> kvp in selectedProcess.Variables)
            {
                if (kvp.Value.Temporary) continue;
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
            if (sender == tsmiConsole)
            {
                ShowConsoleForm();
            }
            if (sender == tsmiGraph)
            {
                ShowGraphForm();
            }
            else if (sender == tsmiFileNew)
            {
                byte[] data = this.FileHandler.NewFile(GetStateData());
                LoadStateData(data);
            }
            else if (sender == tsmiFileOpen)
            {
                byte[] data = this.FileHandler.OpenFile(GetStateData());
                LoadStateData(data);
            }
            else if (sender == tsmiFileSave)
            {
                FileHandler.SaveFile(GetStateData());
            }
            else if (sender == tsmiFileSaveAs)
            {
                FileHandler.SaveFileAs(GetStateData());
            }
          
        }

        private void shortcutButton_Click(object sender, System.EventArgs e)
        {
            if (sender == btnShortcutNew)
            {
                tsmiFileNew.PerformClick();
            }
            else if (sender == btnShortcutOpen)
            {
               tsmiFileOpen.PerformClick();
            }
            else if (sender == btnShortcutSave)
            {
                tsmiFileSave.PerformClick();
            }
            else if (sender == btnShortcutConsole)
            {
                ShowConsoleForm();
            }
            else if (sender == btnShortcutNewScript)
            {
                ShowScriptEditorForm(false);
            }
            else if (sender == btnShortcutOpenScript)
            {
                ShowScriptEditorForm(true);
            }
            else if (sender == btnGraph)
            {
                ShowGraphForm();
            }
        }

        private void exampleButton_Click(object sender, System.EventArgs e)
        {
            string scriptsDir = Path.GetDirectoryName(Application.ExecutablePath);
            if (sender == btnExamplesVariables)
            {
                this.ShowScriptEditorForm(false, $"{scriptsDir}//Resources//Scripts//variables.script");
            }
            else if (sender == btnExamplesConditions)
            {
                this.ShowScriptEditorForm(false, $"{scriptsDir}//Resources//Scripts//conditions.script");
            }
            else if (sender == btnExamplesLoops)
            {
                this.ShowScriptEditorForm(false, $"{scriptsDir}//Resources//Scripts//loops.script");
            }
            else if (sender == btnExamplesFunctions)
            {
                this.ShowScriptEditorForm(false, $"{scriptsDir}//Resources//Scripts//functions.script");
            }
            else if (sender == btnExamplesVectors)
            {
                this.ShowScriptEditorForm(false, $"{scriptsDir}//Resources//Scripts//vectors.script");
            }
            else if (sender == btnExamplePrimes)
            {
                this.ShowScriptEditorForm(false, $"{scriptsDir}//Resources//Scripts//primes.script");
            }
            else if (sender == btnExamplesBubblesort)
            {
                this.ShowScriptEditorForm(false, $"{scriptsDir}//Resources//Scripts//bubblesort.script");
            }
        }

        private void ts_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsBtnAddProcess)
            {
                BaseTextBoxForm txtForm = new BaseTextBoxForm();
                txtForm.Text = "Process name";
                txtForm.Value = "";
                if (txtForm.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(txtForm.Value))
                {
                    if (ProcessManager.Processes.ContainsKey(txtForm.Value))
                    {
                        MessageBox.Show($"Process with name {txtForm.Value} already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string newProcessId = txtForm.Value;
                        ProcessManager.Processes.Add(newProcessId, new Process(newProcessId));
                        LoadProcesses();
                    }
                }
            }
            else if (e.ClickedItem == tsBtnDelProcess)
            {
                Process selectedProcess = lbProcesses.SelectedItem as Process;
                if (selectedProcess != null)
                {
                    ProcessManager.Processes.Remove(selectedProcess.Id);
                    LoadProcesses();
                }
            }
        }




        #endregion

        #endregion

        
    }
}
