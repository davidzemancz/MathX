using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Windows.Forms;
using Base.Api;
using Base.UI.Api.Controls;

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
         
        }

        #endregion

        #region PRIVATE METHODS

        #region Actions

        private void ShowConsoleForm()
        {
            var form = new FormMathXConsole();
            form.Show();
        }

        private void ShowScriptEditorForm()
        {
            var form = new FormMathXScriptEditor();
            form.Show();
        }

        #endregion

        #region Form

        private void menuStrip_ItemClicked(object sender, System.EventArgs e)
        {
            if(sender == consoleToolStripMenuItem)
            {
                this.ShowConsoleForm();
            }
        }

        private void shortcutButton_Click(object sender, System.EventArgs e)
        {
            if (sender == btnShortcutConsole)
            {
                this.ShowConsoleForm();
            }
            else if (sender == btnShortcutNewScript)
            {
                this.ShowScriptEditorForm();
            }
            else if (sender == btnShortcutOpenScript)
            {
                this.ShowScriptEditorForm();
            }
            else if (sender == btnShortcutLastScript)
            {
                this.ShowScriptEditorForm();
            }
        }

        #endregion

        #endregion


    }
}
