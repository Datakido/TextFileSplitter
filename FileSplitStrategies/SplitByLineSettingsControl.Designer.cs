namespace Datakido.FileSplitStrategies
{
    partial class SplitByLineSettingsControl
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLines = new System.Windows.Forms.TextBox();
            this.textboxProvider1 = new XPControls.TextboxProvider();
            this.epLines = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epLines)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select how many lines per file chunk";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtLines
            // 
            this.txtLines.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLines.Location = new System.Drawing.Point(24, 48);
            this.txtLines.Name = "txtLines";
            this.textboxProvider1.SetRenderTextbox(this.txtLines, true);
            this.txtLines.Size = new System.Drawing.Size(187, 13);
            this.textboxProvider1.SetStyle(this.txtLines, XPControls.Style.Rounded);
            this.txtLines.TabIndex = 1;
            this.txtLines.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLines.WordWrap = false;
            this.txtLines.TextChanged += new System.EventHandler(this.txtLines_TextChanged);
            // 
            // epLines
            // 
            this.epLines.ContainerControl = this;
            // 
            // SplitByLineSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.txtLines);
            this.Controls.Add(this.label1);
            this.Name = "SplitByLineSettingsControl";
            this.Size = new System.Drawing.Size(248, 117);
            ((System.ComponentModel.ISupportInitialize)(this.epLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtLines;
        private XPControls.TextboxProvider textboxProvider1;
        private System.Windows.Forms.ErrorProvider epLines;
    }
}