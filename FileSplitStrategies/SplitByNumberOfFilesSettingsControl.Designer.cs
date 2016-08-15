namespace SystemWidgets.FileSplitStrategies
{
    partial class SplitByNumberOfFilesSettingsControl
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
            this.txtChunkCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxProvider1 = new XPControls.TextboxProvider();
            this.epFiles = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // txtChunkCount
            // 
            this.txtChunkCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChunkCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChunkCount.Location = new System.Drawing.Point(35, 54);
            this.txtChunkCount.Name = "txtChunkCount";
            this.textboxProvider1.SetRenderTextbox(this.txtChunkCount, true);
            this.txtChunkCount.Size = new System.Drawing.Size(316, 13);
            this.textboxProvider1.SetStyle(this.txtChunkCount, XPControls.Style.Rounded);
            this.txtChunkCount.TabIndex = 3;
            this.txtChunkCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtChunkCount.WordWrap = false;
            this.txtChunkCount.TextChanged += new System.EventHandler(this.txtChunkCount_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(32, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select the number of split files";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // epFiles
            // 
            this.epFiles.ContainerControl = this;
            // 
            // SplitByNumberOfFilesSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtChunkCount);
            this.Controls.Add(this.label1);
            this.Name = "SplitByNumberOfFilesSettingsControl";
            this.Size = new System.Drawing.Size(386, 101);
            ((System.ComponentModel.ISupportInitialize)(this.epFiles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtChunkCount;
        private System.Windows.Forms.Label label1;
        private XPControls.TextboxProvider textboxProvider1;
        private System.Windows.Forms.ErrorProvider epFiles;
    }
}
