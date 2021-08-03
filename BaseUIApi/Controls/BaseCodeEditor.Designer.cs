
namespace Base.UI.Api.Controls
{
    partial class BaseCodeEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txt = new System.Windows.Forms.RichTextBox();
            this.ts = new System.Windows.Forms.ToolStrip();
            this.tsBtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.tsBtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.ts.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt
            // 
            this.txt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(44)))));
            this.txt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txt.ForeColor = System.Drawing.Color.White;
            this.txt.Location = new System.Drawing.Point(0, 0);
            this.txt.Margin = new System.Windows.Forms.Padding(0);
            this.txt.Name = "txt";
            this.txt.Size = new System.Drawing.Size(991, 470);
            this.txt.TabIndex = 0;
            this.txt.Text = "";
            this.txt.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_KeyDown);
            // 
            // ts
            // 
            this.ts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(73)))), ((int)(((byte)(171)))));
            this.ts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnZoomOut,
            this.tsBtnZoomIn});
            this.ts.Location = new System.Drawing.Point(0, 470);
            this.ts.Name = "ts";
            this.ts.Size = new System.Drawing.Size(991, 25);
            this.ts.TabIndex = 1;
            this.ts.Text = "toolStrip1";
            this.ts.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ts_ItemClicked);
            // 
            // tsBtnZoomOut
            // 
            this.tsBtnZoomOut.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnZoomOut.Image = global::Base.UI.Api.Properties.Resources.zoomOut24;
            this.tsBtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnZoomOut.Name = "tsBtnZoomOut";
            this.tsBtnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.tsBtnZoomOut.Text = "Zoom out [Ctrl + -]";
            // 
            // tsBtnZoomIn
            // 
            this.tsBtnZoomIn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBtnZoomIn.Image = global::Base.UI.Api.Properties.Resources.zoomIn24;
            this.tsBtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnZoomIn.Name = "tsBtnZoomIn";
            this.tsBtnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.tsBtnZoomIn.Text = "Zoom in [Ctrl + +]";
            // 
            // BaseCodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ts);
            this.Controls.Add(this.txt);
            this.Name = "BaseCodeEditor";
            this.Size = new System.Drawing.Size(991, 495);
            this.ts.ResumeLayout(false);
            this.ts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txt;
        private System.Windows.Forms.ToolStrip ts;
        private System.Windows.Forms.ToolStripButton tsBtnZoomOut;
        private System.Windows.Forms.ToolStripButton tsBtnZoomIn;
    }
}
