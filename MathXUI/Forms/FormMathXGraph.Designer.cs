
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bwCoordinates = new System.ComponentModel.BackgroundWorker();
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
            this.pcbGraph.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pcbGraph.Location = new System.Drawing.Point(0, 0);
            this.pcbGraph.Name = "pcbGraph";
            this.pcbGraph.Size = new System.Drawing.Size(951, 470);
            this.pcbGraph.TabIndex = 8;
            this.pcbGraph.TabStop = false;
            this.pcbGraph.SizeChanged += new System.EventHandler(this.pcbGraph_SizeChanged);
            // 
            // mainMenu
            // 
            this.mainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(73)))), ((int)(((byte)(171)))));
            this.mainMenu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.processToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(975, 25);
            this.mainMenu.TabIndex = 7;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(65, 21);
            this.processToolStripMenuItem.Text = "Process";
            // 
            // FormMathXGraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 538);
            this.Controls.Add(this.mainMenu);
            this.Controls.Add(this.pcbContainer);
            this.Name = "FormMathXGraph";
            this.Text = "Graph";
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
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.PictureBox pcbGraph;
        private System.ComponentModel.BackgroundWorker bwCoordinates;
    }
}