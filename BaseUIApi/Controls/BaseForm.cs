using System.Drawing;
using System.Windows.Forms;

namespace Base.UI.Api.Controls
{
    public class BaseForm : Form
    {
        public enum FormModeEnum
        {
            None     = 0,
            Add      = 0b00000001,
            Edit     = 0b00000010,
            Read     = 0b00000100,
            List     = 0b00001000,
            Select   = 0b00010000,
        }

        public BaseForm()
        {
            this.BackColor = Color.FromArgb(33,33,33);
            this.KeyPreview = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!e.Handled)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
        }
    }
}
