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

        #endregion

        #region CONSTRUCTORS

        public FormMathXScriptEditor()
        {
            InitializeComponent();
        }

        #endregion

        #region PRIVATE METHODS

        #region Actions

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
                codeEditor.Text = File.ReadAllText(FileName, Settings.Encoding);
            }
        }

        private void SaveFile()
        {
            if (string.IsNullOrEmpty(FileName) || !File.Exists(FileName))
            {
                this.SaveFileAs();
            }
            else
            {
                File.WriteAllText(FileName, codeEditor.Text, Settings.Encoding);
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
                File.WriteAllText(FileName, codeEditor.Text, Settings.Encoding);
                UnsavedChanges = false;
            }
        }

        #endregion

        #region Form

        private void FormMathXScriptEditor_Load(object sender, EventArgs e)
        {
            FileName = "";
            UnsavedChanges = false;
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
        }

        private void FormMathXScriptEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            CheckUnsavedChanges(out bool cancel);
            e.Cancel = cancel;
        }

        private void codeEditor_TextChanged(object sender, EventArgs e)
        {
            this.UnsavedChanges = true;
        }

        private void menuStrip_ItemClicked(object sender, System.EventArgs e)
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
        }



        #endregion

        #endregion

       
    }
}
