
namespace MathX.UI.Forms
{
    partial class FormMathXMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbProcesses = new System.Windows.Forms.ListBox();
            this.gbProcesses = new System.Windows.Forms.GroupBox();
            this.lbProcessState = new System.Windows.Forms.Label();
            this.lbProcessMemory = new System.Windows.Forms.Label();
            this.lbProcessVarsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbShortcuts = new System.Windows.Forms.GroupBox();
            this.btnShortcutOpenScript = new System.Windows.Forms.Button();
            this.btnShortcutLastScript = new System.Windows.Forms.Button();
            this.btnShortcutNewScript = new System.Windows.Forms.Button();
            this.btnShortcutConsole = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.gbProcesses.SuspendLayout();
            this.gbShortcuts.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(73)))), ((int)(((byte)(171)))));
            this.mainMenu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(984, 25);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consoleToolStripMenuItem});
            this.viewToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // consoleToolStripMenuItem
            // 
            this.consoleToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(73)))), ((int)(((byte)(171)))));
            this.consoleToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.consoleToolStripMenuItem.Name = "consoleToolStripMenuItem";
            this.consoleToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.consoleToolStripMenuItem.Text = "Console";
            this.consoleToolStripMenuItem.Click += new System.EventHandler(this.menuStrip_ItemClicked);
            // 
            // lbProcesses
            // 
            this.lbProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProcesses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lbProcesses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbProcesses.ForeColor = System.Drawing.Color.White;
            this.lbProcesses.FormattingEnabled = true;
            this.lbProcesses.ItemHeight = 15;
            this.lbProcesses.Items.AddRange(new object[] {
            "Process #1 [Running]",
            "Process #2 [Stopped]",
            "Process #3 [Running]"});
            this.lbProcesses.Location = new System.Drawing.Point(6, 38);
            this.lbProcesses.Name = "lbProcesses";
            this.lbProcesses.Size = new System.Drawing.Size(234, 287);
            this.lbProcesses.TabIndex = 1;
            // 
            // gbProcesses
            // 
            this.gbProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gbProcesses.Controls.Add(this.lbProcessState);
            this.gbProcesses.Controls.Add(this.lbProcessMemory);
            this.gbProcesses.Controls.Add(this.lbProcessVarsCount);
            this.gbProcesses.Controls.Add(this.label2);
            this.gbProcesses.Controls.Add(this.label1);
            this.gbProcesses.Controls.Add(this.lbProcesses);
            this.gbProcesses.ForeColor = System.Drawing.Color.White;
            this.gbProcesses.Location = new System.Drawing.Point(12, 38);
            this.gbProcesses.Name = "gbProcesses";
            this.gbProcesses.Size = new System.Drawing.Size(246, 511);
            this.gbProcesses.TabIndex = 2;
            this.gbProcesses.TabStop = false;
            this.gbProcesses.Text = "Processes";
            // 
            // lbProcessState
            // 
            this.lbProcessState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbProcessState.AutoSize = true;
            this.lbProcessState.Location = new System.Drawing.Point(20, 365);
            this.lbProcessState.Name = "lbProcessState";
            this.lbProcessState.Size = new System.Drawing.Size(36, 15);
            this.lbProcessState.TabIndex = 6;
            this.lbProcessState.Text = "State:";
            // 
            // lbProcessMemory
            // 
            this.lbProcessMemory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbProcessMemory.AutoSize = true;
            this.lbProcessMemory.Location = new System.Drawing.Point(20, 401);
            this.lbProcessMemory.Name = "lbProcessMemory";
            this.lbProcessMemory.Size = new System.Drawing.Size(106, 15);
            this.lbProcessMemory.TabIndex = 5;
            this.lbProcessMemory.Text = "Memory allocated:";
            // 
            // lbProcessVarsCount
            // 
            this.lbProcessVarsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbProcessVarsCount.AutoSize = true;
            this.lbProcessVarsCount.Location = new System.Drawing.Point(20, 383);
            this.lbProcessVarsCount.Name = "lbProcessVarsCount";
            this.lbProcessVarsCount.Size = new System.Drawing.Size(90, 15);
            this.lbProcessVarsCount.TabIndex = 4;
            this.lbProcessVarsCount.Text = "Variables count:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 347);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "List";
            // 
            // gbShortcuts
            // 
            this.gbShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShortcuts.Controls.Add(this.btnShortcutOpenScript);
            this.gbShortcuts.Controls.Add(this.btnShortcutLastScript);
            this.gbShortcuts.Controls.Add(this.btnShortcutNewScript);
            this.gbShortcuts.Controls.Add(this.btnShortcutConsole);
            this.gbShortcuts.ForeColor = System.Drawing.Color.White;
            this.gbShortcuts.Location = new System.Drawing.Point(264, 38);
            this.gbShortcuts.Name = "gbShortcuts";
            this.gbShortcuts.Size = new System.Drawing.Size(708, 115);
            this.gbShortcuts.TabIndex = 3;
            this.gbShortcuts.TabStop = false;
            this.gbShortcuts.Text = "Shortcuts";
            // 
            // btnShortcutOpenScript
            // 
            this.btnShortcutOpenScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShortcutOpenScript.Location = new System.Drawing.Point(17, 49);
            this.btnShortcutOpenScript.Name = "btnShortcutOpenScript";
            this.btnShortcutOpenScript.Size = new System.Drawing.Size(143, 23);
            this.btnShortcutOpenScript.TabIndex = 3;
            this.btnShortcutOpenScript.Text = "Open script";
            this.btnShortcutOpenScript.UseVisualStyleBackColor = true;
            this.btnShortcutOpenScript.Click += new System.EventHandler(this.shortcutButton_Click);
            // 
            // btnShortcutLastScript
            // 
            this.btnShortcutLastScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShortcutLastScript.Location = new System.Drawing.Point(17, 78);
            this.btnShortcutLastScript.Name = "btnShortcutLastScript";
            this.btnShortcutLastScript.Size = new System.Drawing.Size(143, 23);
            this.btnShortcutLastScript.TabIndex = 2;
            this.btnShortcutLastScript.Text = "Last script";
            this.btnShortcutLastScript.UseVisualStyleBackColor = true;
            this.btnShortcutLastScript.Click += new System.EventHandler(this.shortcutButton_Click);
            // 
            // btnShortcutNewScript
            // 
            this.btnShortcutNewScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShortcutNewScript.Location = new System.Drawing.Point(17, 20);
            this.btnShortcutNewScript.Name = "btnShortcutNewScript";
            this.btnShortcutNewScript.Size = new System.Drawing.Size(143, 23);
            this.btnShortcutNewScript.TabIndex = 1;
            this.btnShortcutNewScript.Text = "New script";
            this.btnShortcutNewScript.UseVisualStyleBackColor = true;
            this.btnShortcutNewScript.Click += new System.EventHandler(this.shortcutButton_Click);
            // 
            // btnShortcutConsole
            // 
            this.btnShortcutConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShortcutConsole.Location = new System.Drawing.Point(166, 20);
            this.btnShortcutConsole.Name = "btnShortcutConsole";
            this.btnShortcutConsole.Size = new System.Drawing.Size(143, 23);
            this.btnShortcutConsole.TabIndex = 0;
            this.btnShortcutConsole.Text = "Start console";
            this.btnShortcutConsole.UseVisualStyleBackColor = true;
            this.btnShortcutConsole.Click += new System.EventHandler(this.shortcutButton_Click);
            // 
            // FormMathXMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gbShortcuts);
            this.Controls.Add(this.gbProcesses);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "FormMathXMain";
            this.Text = "MathX";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.gbProcesses.ResumeLayout(false);
            this.gbProcesses.PerformLayout();
            this.gbShortcuts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consoleToolStripMenuItem;
        private System.Windows.Forms.ListBox lbProcesses;
        private System.Windows.Forms.GroupBox gbProcesses;
        private System.Windows.Forms.Label lbProcessState;
        private System.Windows.Forms.Label lbProcessMemory;
        private System.Windows.Forms.Label lbProcessVarsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbShortcuts;
        private System.Windows.Forms.Button btnShortcutConsole;
        private System.Windows.Forms.Button btnShortcutNewScript;
        private System.Windows.Forms.Button btnShortcutLastScript;
        private System.Windows.Forms.Button btnShortcutOpenScript;
    }
}

