using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Base.UI.Api.Controls;
using Base.Api;
using MathX.Processes;
using Base.UI.Api.Utils;

namespace MathX.UI.Forms
{
    public partial class FormMathXScriptEditor : BaseForm
    {
        #region PROPS

        #endregion

        #region FIELDS

        private bool _openingFile;
        private Process _currentProcess;

        #endregion

        #region CONSTRUCTORS

        public FormMathXScriptEditor()
        {
            InitializeComponent();
            FileHandler = new BaseFileHandler(this, Settings.Encoding, "Script files (*.txt;*.script)|*.txt;*.script|All files (*.*)|*.*");
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


        private void Run()
        {
            if (FileHandler.UnsavedChanges)
            {
                FileHandler.SaveFile(codeEditor.Text);
            }
            using (FileStream fileStream = new FileStream(FileHandler.FileName, FileMode.Open, FileAccess.Read))
            {
                long position = _currentProcess.Input.Position;
                fileStream.CopyTo(_currentProcess.Input);
                _currentProcess.Input.Seek(position, SeekOrigin.Begin);
                _currentProcess.Run(out BaseStatus status);
                _currentProcess.Output.Seek(0, SeekOrigin.Begin);
                string output = _currentProcess.OutputReader.ReadToEnd();
                txtOutput.Text = output + status;
            }
        }

        #endregion

        #region Form

        private void FormMathXScriptEditor_Activated(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void FormMathXScriptEditor_Load(object sender, EventArgs e)
        {

            if (((Input)FormInput).ShowOpenFileDialog)
            {
                FileHandler.OpenFile("");
            }
            else if (!string.IsNullOrEmpty(((Input)FormInput).FileName))
            {
                _openingFile = true;
                codeEditor.WriteText(File.ReadAllText(((Input)FormInput).FileName, Settings.Encoding));
                _openingFile = false;
            }

        }

        private void FormMathXScriptEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                codeEditor.Text = FileHandler.NewFile(codeEditor.Text);
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                _openingFile = true;
                codeEditor.Text = FileHandler.OpenFile(codeEditor.Text);
                _openingFile = false;
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                FileHandler.SaveFile(codeEditor.Text);
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                FileHandler.SaveFileAs(codeEditor.Text);
            }
            else if (e.KeyCode == Keys.F5)
            {
                Run();
            }
        }

        private void FormMathXScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            FileHandler.CheckUnsavedChanges(codeEditor.Text, out bool cancel);
            e.Cancel = cancel;
        }

        private void codeEditor_TextChanged(object sender, EventArgs e)
        {
            if (!_openingFile)
            {
                FileHandler.UnsavedChanges = true;
            }
        }

        private void menuStrip_ItemClicked(object sender, EventArgs e)
        {
            if (sender == tsmiFileNew)
            {
                codeEditor.Text = FileHandler.NewFile(codeEditor.Text);
            }
            else if (sender == tsmiFileOpen)
            {
                _openingFile = true;
                codeEditor.Text = FileHandler.OpenFile(codeEditor.Text);
                _openingFile = false;
            }
            else if (sender == tsmiFileSave)
            {
                FileHandler.SaveFile(codeEditor.Text);
            }
            else if (sender == tsmiFileSaveAs)
            {
                FileHandler.SaveFileAs(codeEditor.Text);
            }
            else if (sender == tsmiRun)
            {
                Run();
            }
        }

        private void cbxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentProcess = cbxProcesses.SelectedItem as Process;
        }

        #endregion

        #endregion

        #region CLASSES

        public class Input : BaseFormInput
        {
            public bool ShowOpenFileDialog { get; set; }

            public string FileName { get; set; }
        }


        #endregion

       
    }
}
