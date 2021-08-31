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

namespace MathX.UI.Forms
{
    public partial class FormMathXScriptEditor : BaseForm
    {
        #region PROPS

        private string FileName 
        {
            get
            {
                return _fileName;
            }
            set 
            {
                bool valueChanged = value != _fileName;
                _fileName = value;
                if (valueChanged) UpdateTitleText();
            }
        }

        private bool UnsavedChanges
        {
            get
            {
                return _unsavedChanges;
            }
            set
            {
                bool valueChanged = value != _unsavedChanges;
                _unsavedChanges = value;
                if(valueChanged) UpdateTitleText();
            }
        }
       
        #endregion

        #region FIELDS

        private string _fileName;
        private bool _unsavedChanges;
        private bool _supressTextChanged;
        private Process _currentProcess;

        #endregion

        #region CONSTRUCTORS

        public FormMathXScriptEditor()
        {
            InitializeComponent();
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

        private void UpdateTitleText()
        {
            Text = (UnsavedChanges ? "*" : "")
                       + (string.IsNullOrEmpty(FileName) ? "Untitled" : Path.GetFileName(FileName))
                       + " - MathX Script editor";
        }

        private void CheckUnsavedChanges(out bool cancel)
        {
            cancel = false;
            if (UnsavedChanges)
            {
                DialogResult dr = MessageBox.Show("Do you want to save changes to " + (string.IsNullOrEmpty(FileName) ? "Untitled" : FileName) + "?", "MathX Script editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if(dr == DialogResult.Cancel)
                {
                    cancel = true;
                }
                else if(dr == DialogResult.Yes)
                {
                    this.SaveFile();
                }
            }
        }

        private void NewFile()
        {
            CheckUnsavedChanges(out bool cancel);
            if (cancel) return;

            FileName = "";
            codeEditor.Text = "";
        }

        private void OpenFile()
        {
            CheckUnsavedChanges(out bool cancel);
            if (cancel) return;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Script files (*.txt;*.script)|*.txt;*.script|All files (*.*)|*.*";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileName = ofd.FileName;
                _supressTextChanged = true;
                codeEditor.WriteText(File.ReadAllText(FileName, Settings.Encoding));
                _supressTextChanged = false;
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(_fileName) || !File.Exists(_fileName))
            {
                this.SaveFileAs();
            }
            else
            {
                File.WriteAllText(_fileName, codeEditor.Text, Settings.Encoding);
                UnsavedChanges = false;
            }
        }

        private void SaveFileAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Script files (*.txt;*.script)|*.txt;*.script|All files (*.*)|*.*";
            sfd.FilterIndex = 1;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileName = sfd.FileName;
                File.WriteAllText(_fileName, codeEditor.Text, Settings.Encoding);
                UnsavedChanges = false;
            }
        }

        private void Run()
        {
            if (this.UnsavedChanges)
            {
                this.SaveFile();
            }
            using (FileStream fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read))
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
            FileName = "";
            UnsavedChanges = false;

            if (((Input)this.FormInput).ShowOpenFileDialog)
            {
                this.OpenFile();
            }
            else if (!string.IsNullOrEmpty(((Input)this.FormInput).FileName))
            {
                FileName = ((Input)this.FormInput).FileName;
                _supressTextChanged = true;
                codeEditor.WriteText(File.ReadAllText(FileName, Settings.Encoding));
                _supressTextChanged = false;
            }

        }

        private void FormMathXScriptEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                this.NewFile();
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                this.OpenFile();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                this.SaveFile();
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.S)
            {
                this.SaveFileAs();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.Run();
            }
        }

        private void FormMathXScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            CheckUnsavedChanges(out bool cancel);
            e.Cancel = cancel;
        }

        private void codeEditor_TextChanged(object sender, EventArgs e)
        {
            if (!_supressTextChanged)
            {
                this.UnsavedChanges = true;
            }
        }

        private void menuStrip_ItemClicked(object sender, EventArgs e)
        {
            if (sender == tsmiFileNew)
            {
                this.NewFile();
            }
            else if (sender == tsmiFileOpen)
            {
                this.OpenFile();
            }
            else if (sender == tsmiFileSave)
            {
                this.SaveFile();
            }
            else if (sender == tsmiFileSaveAs)
            {
                this.SaveFileAs();
            }
            else if (sender == tsmiRun)
            {
                this.Run();
            }
        }

        private void cbxProcesses_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentProcess = cbxProcesses.SelectedItem as Process;
        }

        #endregion

        #endregion

        #region CLASSES

        public class Input : BaseInput
        {
            public bool ShowOpenFileDialog { get; set; }

            public string FileName { get; set; }
        }


        #endregion

       
    }
}
