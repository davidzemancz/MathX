
namespace MathX.UI.Forms
{
    partial class FormMathXGraph
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pcbContainer = new System.Windows.Forms.PictureBox();
            this.pcbGraph = new System.Windows.Forms.PictureBox();
            this.bwCoordinates = new System.ComponentModel.BackgroundWorker();
            this.cbxFunctions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.cbxProcesses = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbContainer)).BeginInit();
            this.pcbContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbGraph)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcbContainer
            // 
            this.pcbContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcbContainer.Controls.Add(this.pcbGraph);
            this.pcbContainer.Location = new System.Drawing.Point(12, 28);
            this.pcbContainer.Name = "pcbContainer";
            this.pcbContainer.Size = new System.Drawing.Size(951, 470);
            this.pcbContainer.TabIndex = 0;
            this.pcbContainer.TabStop = false;
            // 
            // pcbGraph
            // 
            this.pcbGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pcbGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pcbGraph.Location = new System.Drawing.Point(0, 2);
            this.pcbGraph.Name = "pcbGraph";
            this.pcbGraph.Size = new System.Drawing.Size(951, 468);
            this.pcbGraph.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pcbGraph.TabIndex = 8;
            this.pcbGraph.TabStop = false;
            this.pcbGraph.SizeChanged += new System.EventHandler(this.pcbGraph_SizeChanged);
            // 
            // cbxFunctions
            // 
            this.cbxFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFunctions.FormattingEnabled = true;
            this.cbxFunctions.Location = new System.Drawing.Point(76, 504);
            this.cbxFunctions.Name = "cbxFunctions";
            this.cbxFunctions.Size = new System.Drawing.Size(249, 23);
            this.cbxFunctions.TabIndex = 8;
            this.cbxFunctions.SelectedIndexChanged += new System.EventHandler(this.cbxFunctions_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 507);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Function:";
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(73)))), ((int)(((byte)(171)))));
            this.mainMenu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbxProcesses});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(975, 27);
            this.mainMenu.TabIndex = 10;
            this.mainMenu.Text = "menuStrip1";
            // 
            // cbxProcesses
            // 
            this.cbxProcesses.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProcesses.Name = "cbxProcesses";
            this.cbxProcesses.Size = new System.Drawing.Size(200, 23);
            this.cbxProcesses.SelectedIndexChanged += new System.EventHandler(this.cbxProcesses_SelectedIndexChanged);
            // 
            // FormMathXGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 538);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxFunctions);
            this.Controls.Add(this.pcbContainer);
            this.Name = "FormMathXGraph";
            this.Text = "Graph";
            this.Activated += new System.EventHandler(this.FormMathXGraph_Activated);
            this.Load += new System.EventHandler(this.FormMathXGraph_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbContainer)).EndInit();
            this.pcbContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pcbGraph)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbContainer;
        private System.Windows.Forms.PictureBox pcbGraph;
        private System.ComponentModel.BackgroundWorker bwCoordinates;
        private System.Windows.Forms.ComboBox cbxFunctions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripComboBox cbxProcesses;
    }
}