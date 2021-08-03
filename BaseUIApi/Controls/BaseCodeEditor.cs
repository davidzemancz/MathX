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
        public override string Text
        {
            get { return txt.Text; }
            set { txt.Text = value; }
        }

        [Browsable(true)]
        public new event EventHandler TextChanged;

        public BaseCodeEditor()
        {
            InitializeComponent();
        }

        private void ParseLine(ref string line, int lineNumber)
        {
            int lineStart = txt.GetFirstCharIndexFromLine(lineNumber);
            
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '=')
                {
                    ChangeTextStyle(lineStart + i, 1, lineStart + line.Length, Color.Green, txt.BackColor);
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    ChangeTextStyle(lineStart + i, 1, lineStart + line.Length, Color.Blue, txt.BackColor);
                }
            }
        }

        private void ChangeTextStyle(int startIndex, int length, int lineEnd, Color foreColor, Color backColor)
        {
            txt.SelectionStart = startIndex;
            txt.SelectionLength = length;
            txt.SelectionColor = foreColor;
            txt.SelectionBackColor = backColor;
            txt.SelectionLength = 0;
            txt.SelectionStart = lineEnd;
            txt.SelectionColor = txt.ForeColor;
            txt.SelectionBackColor = txt.BackColor;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            int lineNumber = txt.GetLineFromCharIndex(txt.SelectionStart);
            if (lineNumber >= 0 && lineNumber < txt.Lines.Length)
            {
                ParseLine(ref txt.Lines[lineNumber], lineNumber);
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
