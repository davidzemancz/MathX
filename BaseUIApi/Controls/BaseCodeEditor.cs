using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base.UI.Api.Controls
{
    public partial class BaseCodeEditor : UserControl
    {
        [Browsable(true)]
        public new event EventHandler TextChanged;

        public override string Text { get => txt.Text ; set => txt.Text = value; }

        public BaseCodeEditor()
        {
            InitializeComponent();
        }

        public void WriteLine(string line)
        {
            txt.Text += $"{Environment.NewLine}{line}";
            ParseLine(txt.Lines.Length - 1);
        }

        public void WriteText(string text)
        {
            txt.Text = text;
            for (int i = 0; i < txt.Lines.Length; i++)
            {
                ParseLine(i);
            }
        }

        private void ParseLine(int lineIndex)
        {
            string line = txt.Lines[lineIndex];
            int lineStart = txt.GetFirstCharIndexFromLine(lineIndex);
            
            //for (int i = 0; i < line.Length; i++)
            //{
            //    char c = line[i];
            //    if (c == '=')
            //    {
            //        ChangeTextStyle(lineStart + i, 1, lineStart + line.Length, Color.Green, txt.BackColor);
            //    }
            //    else if (c == '+' || c == '-' || c == '*' || c == '/')
            //    {
            //        ChangeTextStyle(lineStart + i, 1, lineStart + line.Length, Color.Blue, txt.BackColor);
            //    }
            //    else if(i > 1)
            //    {
            //        if(c == 'r' && line[i - 1] == 'o' && line[i - 2] == 'f')
            //        {
            //            ChangeTextStyle(lineStart + i - 2, 3, lineStart + line.Length, Color.Blue, txt.BackColor);
            //        }
            //    }
            //}
        }

        private void ChangeTextStyle(int startIndex, int length, int lineEndIndex, Color foreColor, Color backColor)
        {
            txt.SelectionStart = startIndex;
            txt.SelectionLength = length;
            txt.SelectionColor = foreColor;
            txt.SelectionBackColor = backColor;
            txt.SelectionLength = 0;
            txt.SelectionStart = lineEndIndex;
            txt.SelectionColor = txt.ForeColor;
            txt.SelectionBackColor = txt.BackColor;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            int lineNumber = txt.GetLineFromCharIndex(txt.SelectionStart);
            if (lineNumber >= 0 && lineNumber < txt.Lines.Length)
            {
                ParseLine(lineNumber);
            }

            TextChanged?.Invoke(this, e);
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Add)
            {
                tsBtnZoomIn.PerformClick();
            }
            else if (e.Control && e.KeyCode == Keys.Subtract)
            {
                tsBtnZoomOut.PerformClick();
            }
        }

        private void ts_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsBtnZoomIn)
            {
                txt.ZoomFactor += 0.5f;
            }
            else if (e.ClickedItem == tsBtnZoomOut)
            {
                txt.ZoomFactor -= 0.5f;
            }
        }

       
    }
}
