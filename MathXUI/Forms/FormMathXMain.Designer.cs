
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
            this.tlpProcesses = new System.Windows.Forms.TableLayoutPanel();
            this.lbFunctions = new System.Windows.Forms.ListBox();
            this.lbVariables = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tsProcesses = new System.Windows.Forms.ToolStrip();
            this.tsBtnAddProcess = new System.Windows.Forms.ToolStripButton();
            this.tsBtnDelProcess = new System.Windows.Forms.ToolStripButton();
            this.gbShortcuts = new System.Windows.Forms.GroupBox();
            this.btnGraph = new System.Windows.Forms.Button();
            this.btnShortcutOpenScript = new System.Windows.Forms.Button();
            this.btnShortcutLastScript = new System.Windows.Forms.Button();
            this.btnShortcutNewScript = new System.Windows.Forms.Button();
            this.btnShortcutConsole = new System.Windows.Forms.Button();
            this.gbExamples = new System.Windows.Forms.GroupBox();
            this.btnExamplesFunctions = new System.Windows.Forms.Button();
            this.btnExamplesLoops = new System.Windows.Forms.Button();
            this.btnExamplesConditions = new System.Windows.Forms.Button();
            this.btnExamplesVariables = new System.Windows.Forms.Button();
            this.mainMenu.SuspendLayout();
            this.gbProcesses.SuspendLayout();
            this.tlpProcesses.SuspendLayout();
            this.tsProcesses.SuspendLayout();
            this.gbShortcuts.SuspendLayout();
            this.gbExamples.SuspendLayout();
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
            this.lbProcesses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lbProcesses.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbProcesses.ForeColor = System.Drawing.Color.White;
            this.lbProcesses.FormattingEnabled = true;
            this.lbProcesses.ItemHeight = 15;
            this.lbProcesses.Items.AddRange(new object[] {
            "Process #1 [Running]",
            "Process #2 [Stopped]",
            "Process #3 [Running]"});
            this.lbProcesses.Location = new System.Drawing.Point(3, 3);
            this.lbProcesses.Name = "lbProcesses";
            this.tlpProcesses.SetRowSpan(this.lbProcesses, 2);
            this.lbProcesses.Size = new System.Drawing.Size(301, 213);
            this.lbProcesses.TabIndex = 1;
            this.lbProcesses.SelectedIndexChanged += new System.EventHandler(this.lbProcesses_SelectedIndexChanged);
            // 
            // gbProcesses
            // 
            this.gbProcesses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProcesses.Controls.Add(this.tlpProcesses);
            this.gbProcesses.Controls.Add(this.tsProcesses);
            this.gbProcesses.ForeColor = System.Drawing.Color.White;
            this.gbProcesses.Location = new System.Drawing.Point(12, 41);
            this.gbProcesses.Name = "gbProcesses";
            this.gbProcesses.Size = new System.Drawing.Size(960, 241);
            this.gbProcesses.TabIndex = 2;
            this.gbProcesses.TabStop = false;
            this.gbProcesses.Text = "Processes";
            // 
            // tlpProcesses
            // 
            this.tlpProcesses.ColumnCount = 3;
            this.tlpProcesses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProcesses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProcesses.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlpProcesses.Controls.Add(this.lbFunctions, 2, 1);
            this.tlpProcesses.Controls.Add(this.lbVariables, 1, 1);
            this.tlpProcesses.Controls.Add(this.label2, 1, 0);
            this.tlpProcesses.Controls.Add(this.label3, 2, 0);
            this.tlpProcesses.Controls.Add(this.lbProcesses, 0, 0);
            this.tlpProcesses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpProcesses.Location = new System.Drawing.Point(35, 19);
            this.tlpProcesses.Name = "tlpProcesses";
            this.tlpProcesses.RowCount = 2;
            this.tlpProcesses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpProcesses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpProcesses.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpProcesses.Size = new System.Drawing.Size(922, 219);
            this.tlpProcesses.TabIndex = 5;
            // 
            // lbFunctions
            // 
            this.lbFunctions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lbFunctions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbFunctions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFunctions.ForeColor = System.Drawing.Color.White;
            this.lbFunctions.FormattingEnabled = true;
            this.lbFunctions.ItemHeight = 15;
            this.lbFunctions.Items.AddRange(new object[] {
            "a = 4",
            "b = True",
            "c = [4,3,4;2,2,1]"});
            this.lbFunctions.Location = new System.Drawing.Point(617, 23);
            this.lbFunctions.Name = "lbFunctions";
            this.lbFunctions.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbFunctions.Size = new System.Drawing.Size(302, 193);
            this.lbFunctions.TabIndex = 4;
            // 
            // lbVariables
            // 
            this.lbVariables.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.lbVariables.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbVariables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbVariables.ForeColor = System.Drawing.Color.White;
            this.lbVariables.FormattingEnabled = true;
            this.lbVariables.ItemHeight = 15;
            this.lbVariables.Items.AddRange(new object[] {
            "a = 4",
            "b = True",
            "c = [4,3,4;2,2,1]"});
            this.lbVariables.Location = new System.Drawing.Point(310, 23);
            this.lbVariables.Name = "lbVariables";
            this.lbVariables.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbVariables.Size = new System.Drawing.Size(301, 193);
            this.lbVariables.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Variables";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(617, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Functions";
            // 
            // tsProcesses
            // 
            this.tsProcesses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(73)))), ((int)(((byte)(171)))));
            this.tsProcesses.Dock = System.Windows.Forms.DockStyle.Left;
            this.tsProcesses.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsProcesses.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnAddProcess,
            this.tsBtnDelProcess});
            this.tsProcesses.Location = new System.Drawing.Point(3, 19);
            this.tsProcesses.Name = "tsProcesses";
            this.tsProcesses.Size = new System.Drawing.Size(32, 219);
            this.tsProcesses.TabIndex = 6;
            this.tsProcesses.Text = "toolStrip1";
            this.tsProcesses.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_ItemClicked);
            // 
            // tsBtnAddProcess
            // 
            this.tsBtnAddProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnAddProcess.Image = global::MathX.UI.Properties.Resources.add24;
            this.tsBtnAddProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAddProcess.Name = "tsBtnAddProcess";
            this.tsBtnAddProcess.Size = new System.Drawing.Size(29, 20);
            this.tsBtnAddProcess.Text = "toolStripButton1";
            // 
            // tsBtnDelProcess
            // 
            this.tsBtnDelProcess.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnDelProcess.Image = global::MathX.UI.Properties.Resources.cancel24;
            this.tsBtnDelProcess.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnDelProcess.Name = "tsBtnDelProcess";
            this.tsBtnDelProcess.Size = new System.Drawing.Size(29, 20);
            this.tsBtnDelProcess.Text = "toolStripButton1";
            // 
            // gbShortcuts
            // 
            this.gbShortcuts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShortcuts.Controls.Add(this.btnGraph);
            this.gbShortcuts.Controls.Add(this.btnShortcutOpenScript);
            this.gbShortcuts.Controls.Add(this.btnShortcutLastScript);
            this.gbShortcuts.Controls.Add(this.btnShortcutNewScript);
            this.gbShortcuts.Controls.Add(this.btnShortcutConsole);
            this.gbShortcuts.ForeColor = System.Drawing.Color.White;
            this.gbShortcuts.Location = new System.Drawing.Point(12, 288);
            this.gbShortcuts.Name = "gbShortcuts";
            this.gbShortcuts.Size = new System.Drawing.Size(515, 115);
            this.gbShortcuts.TabIndex = 3;
            this.gbShortcuts.TabStop = false;
            this.gbShortcuts.Text = "Shortcuts";
            // 
            // btnGraph
            // 
            this.btnGraph.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGraph.Location = new System.Drawing.Point(315, 20);
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Size = new System.Drawing.Size(143, 23);
            this.btnGraph.TabIndex = 4;
            this.btnGraph.Text = "Graph";
            this.btnGraph.UseVisualStyleBackColor = true;
            this.btnGraph.Click += new System.EventHandler(this.shortcutButton_Click);
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
            this.btnShortcutConsole.Text = "Console";
            this.btnShortcutConsole.UseVisualStyleBackColor = true;
            this.btnShortcutConsole.Click += new System.EventHandler(this.shortcutButton_Click);
            // 
            // gbExamples
            // 
            this.gbExamples.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbExamples.Controls.Add(this.btnExamplesFunctions);
            this.gbExamples.Controls.Add(this.btnExamplesLoops);
            this.gbExamples.Controls.Add(this.btnExamplesConditions);
            this.gbExamples.Controls.Add(this.btnExamplesVariables);
            this.gbExamples.ForeColor = System.Drawing.Color.White;
            this.gbExamples.Location = new System.Drawing.Point(654, 288);
            this.gbExamples.Name = "gbExamples";
            this.gbExamples.Size = new System.Drawing.Size(323, 115);
            this.gbExamples.TabIndex = 4;
            this.gbExamples.TabStop = false;
            this.gbExamples.Text = "Examples";
            // 
            // btnExamplesFunctions
            // 
            this.btnExamplesFunctions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExamplesFunctions.Location = new System.Drawing.Point(166, 22);
            this.btnExamplesFunctions.Name = "btnExamplesFunctions";
            this.btnExamplesFunctions.Size = new System.Drawing.Size(143, 23);
            this.btnExamplesFunctions.TabIndex = 7;
            this.btnExamplesFunctions.Text = "Functions";
            this.btnExamplesFunctions.UseVisualStyleBackColor = true;
            // 
            // btnExamplesLoops
            // 
            this.btnExamplesLoops.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExamplesLoops.Location = new System.Drawing.Point(17, 80);
            this.btnExamplesLoops.Name = "btnExamplesLoops";
            this.btnExamplesLoops.Size = new System.Drawing.Size(143, 23);
            this.btnExamplesLoops.TabIndex = 6;
            this.btnExamplesLoops.Text = "Loops";
            this.btnExamplesLoops.UseVisualStyleBackColor = true;
            // 
            // btnExamplesConditions
            // 
            this.btnExamplesConditions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExamplesConditions.Location = new System.Drawing.Point(17, 51);
            this.btnExamplesConditions.Name = "btnExamplesConditions";
            this.btnExamplesConditions.Size = new System.Drawing.Size(143, 23);
            this.btnExamplesConditions.TabIndex = 5;
            this.btnExamplesConditions.Text = "Conditions";
            this.btnExamplesConditions.UseVisualStyleBackColor = true;
            // 
            // btnExamplesVariables
            // 
            this.btnExamplesVariables.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExamplesVariables.Location = new System.Drawing.Point(17, 22);
            this.btnExamplesVariables.Name = "btnExamplesVariables";
            this.btnExamplesVariables.Size = new System.Drawing.Size(143, 23);
            this.btnExamplesVariables.TabIndex = 4;
            this.btnExamplesVariables.Text = "Variables";
            this.btnExamplesVariables.UseVisualStyleBackColor = true;
            // 
            // FormMathXMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(33)))), ((int)(((byte)(33)))));
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.gbExamples);
            this.Controls.Add(this.gbShortcuts);
            this.Controls.Add(this.gbProcesses);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "FormMathXMain";
            this.Text = "MathX";
            this.Activated += new System.EventHandler(this.FormMathXMain_Activated);
            this.Load += new System.EventHandler(this.FormMathXMain_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.gbProcesses.ResumeLayout(false);
            this.gbProcesses.PerformLayout();
            this.tlpProcesses.ResumeLayout(false);
            this.tlpProcesses.PerformLayout();
            this.tsProcesses.ResumeLayout(false);
            this.tsProcesses.PerformLayout();
            this.gbShortcuts.ResumeLayout(false);
            this.gbExamples.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox gbShortcuts;
        private System.Windows.Forms.Button btnShortcutConsole;
        private System.Windows.Forms.Button btnShortcutNewScript;
        private System.Windows.Forms.Button btnShortcutLastScript;
        private System.Windows.Forms.Button btnShortcutOpenScript;
        private System.Windows.Forms.GroupBox gbExamples;
        private System.Windows.Forms.Button btnExamplesFunctions;
        private System.Windows.Forms.Button btnExamplesLoops;
        private System.Windows.Forms.Button btnExamplesConditions;
        private System.Windows.Forms.Button btnExamplesVariables;
        private System.Windows.Forms.Button btnGraph;
        private System.Windows.Forms.ListBox lbVariables;
        private System.Windows.Forms.TableLayoutPanel tlpProcesses;
        private System.Windows.Forms.ListBox lbFunctions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStrip tsProcesses;
        private System.Windows.Forms.ToolStripButton tsBtnAddProcess;
        private System.Windows.Forms.ToolStripButton tsBtnDelProcess;
    }
}

