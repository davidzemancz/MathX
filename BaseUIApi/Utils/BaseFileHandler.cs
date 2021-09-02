using Base.UI.Api.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base.UI.Api.Utils
{
    public class BaseFileHandler
    {
        private string _fileName;
        private bool _unsavedChanges;

        public BaseForm Form { get; set; }

        public Encoding Encoding { get; set; }

        public string Filter { get; set; }

        public string FileName
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

        public bool UnsavedChanges
        {
            get
            {
                return _unsavedChanges;
            }
            set
            {
                bool valueChanged = value != _unsavedChanges;
                _unsavedChanges = value;
                if (valueChanged) UpdateTitleText();
            }
        }

        public BaseFileHandler(BaseForm form, Encoding encoding, string filter)
        {
            Form = form;
            Encoding = encoding;
            Filter = filter;
        }

        private void UpdateTitleText()
        {
            Form.Text = (UnsavedChanges ? "*" : "")
                       + (string.IsNullOrEmpty(FileName) ? "Untitled" : Path.GetFileName(FileName))
                       + " - MathX";
        }

        public void CheckUnsavedChanges(string prevText, out bool cancel)
        {
            CheckUnsavedChanges(Encoding.GetBytes(prevText ?? ""), out cancel);
        }

        public void CheckUnsavedChanges(byte[] prevData, out bool cancel)
        {
            cancel = false;
            if (UnsavedChanges && prevData != null && prevData.Length > 0)
            {
                DialogResult dr = MessageBox.Show("Do you want to save changes to " + (string.IsNullOrEmpty(FileName) ? "Untitled" : FileName) + "?", "MathX", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel)
                {
                    cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    this.SaveFile(prevData);
                }
            }
        }

        public string NewFile(string prevText)
        {
            NewFile(Encoding.GetBytes(prevText));
            return null;
        }

        public byte[] NewFile(byte[] prevData)
        {
            CheckUnsavedChanges(prevData, out bool cancel);
            if (cancel) return null;

            FileName = "";
            return null;
        }

        public string OpenFile(string prevText)
        {
            return OpenFile("", prevText);
        }

        public string OpenFile(string fileName, string prevText)
        {
            return Encoding.GetString(OpenFile(fileName, Encoding.GetBytes(prevText)));
        }

        public byte[] OpenFile(byte[] prevData)
        {
            return OpenFile("", prevData);
        }

        public byte[] OpenFile(string fileName, byte[] prevData)
        {
            CheckUnsavedChanges(prevData, out bool cancel);
            if (cancel) return null;

            if (string.IsNullOrEmpty(fileName))
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = Filter;
                ofd.FilterIndex = 1;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofd.FileName;
                    return File.ReadAllBytes(FileName);
                }
            }
            else
            {
                FileName = fileName;
                return File.ReadAllBytes(FileName);
            }
            
            return null;
        }

        public void SaveFile(string text)
        {
            SaveFile(Encoding.GetBytes(text));
        }

        public void SaveFile(byte[] data)
        {
            if (string.IsNullOrEmpty(_fileName) || !File.Exists(_fileName))
            {
                this.SaveFileAs(data);
            }
            else
            {
                File.WriteAllBytes(_fileName, data);
                UnsavedChanges = false;
            }
        }

        public void SaveFileAs(string text)
        {
            SaveFileAs(Encoding.GetBytes(text));
        }

        public void SaveFileAs(byte[] data)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = Filter;
            sfd.FilterIndex = 1;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileName = sfd.FileName;
                File.WriteAllBytes(_fileName, data);
                UnsavedChanges = false;
            }
        }

    }
}
