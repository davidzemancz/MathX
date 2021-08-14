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

        public HashSet<string> KeyWords { get; } = new HashSet<string>() { 
            "while", "endwhile", 
            "if", "else" ,"endif" 
        };

        public BaseCodeEditor()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            txt.SelectionTabs = new int[] { 20, 40, 60, 80 };
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
            this.SuspendLayout();

            string line = txt.Lines[lineIndex];
            int currentIndex = txt.SelectionStart;
            int lineStart = txt.GetFirstCharIndexFromLine(lineIndex);

            // Reset
            ChangeTextStyle(lineStart, line.Length, currentIndex, txt.ForeColor, txt.BackColor);

            // Set style
            StringBuilder wordBuilder = new StringBuilder();
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '\t') continue;

                if (c == ' ' || i == line.Length - 1)
                {
                    if (c != ' ')
                    {
                        wordBuilder.Append(c);
                        i++;
                    }

                    string word = wordBuilder.ToString();
                    if (this.KeyWords.Contains(word))
                    {
                        ChangeTextStyle(lineStart + i - word.Length, word.Length, currentIndex, Color.Blue, txt.BackColor);
                    }

                    wordBuilder = new StringBuilder();
                }
                else
                {
                    wordBuilder.Append(c);
                }
            }

            txt.SelectionBackColor = Color.Blue;

            this.ResumeLayout();
        }

        private void ChangeTextStyle(int startIndex, int length, int currentIndex, Color foreColor, Color backColor)
        {
            txt.SelectionStart = startIndex;
            txt.SelectionLength = length;
            txt.SelectionColor = foreColor;
            txt.SelectionBackColor = backColor;
            txt.SelectionLength = 0;
            txt.SelectionStart = currentIndex;
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
