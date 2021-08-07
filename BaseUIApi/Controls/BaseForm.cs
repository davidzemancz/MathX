using Base.Api;
using System.Drawing;
using System.Windows.Forms;

namespace Base.UI.Api.Controls
{
    public class BaseForm : Form
    {
        public BaseInput FormInput { get; set; }

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
