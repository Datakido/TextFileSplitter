namespace SystemWidgets.FileSplitStrategies
{
    partial class SplitOffChunksAndStopSettingsControl
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.cbMeasure = new System.Windows.Forms.ComboBox();
            this.textboxProvider1 = new XPControls.TextboxProvider();
            this.txtChunkCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(311, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Bytes Per Chunk";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSize
            // 
            this.txtSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSize.Location = new System.Drawing.Point(82, 30);
            this.txtSize.Name = "txtSize";
            this.textboxProvider1.SetRenderTextbox(this.txtSize, true);
            this.txtSize.Size = new System.Drawing.Size(92, 13);
            this.textboxProvider1.SetStyle(this.txtSize, XPControls.Style.Rounded);
            this.txtSize.TabIndex = 15;
            this.txtSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSize.TextChanged += new System.EventHandler(this.txtSize_TextChanged);
            // 
            // cbMeasure
            // 
            this.cbMeasure.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbMeasure.FormattingEnabled = true;
            this.cbMeasure.Items.AddRange(new object[] {
            "KB - Kilobytes",
            "MB - Megabytes",
            "GB - Gigabytes"});
            this.cbMeasure.Location = new System.Drawing.Point(180, 27);
            this.cbMeasure.Name = "cbMeasure";
            this.cbMeasure.Size = new System.Drawing.Size(120, 21);
            this.textboxProvider1.SetStyle(this.cbMeasure, XPControls.Style.Rounded);
            this.cbMeasure.TabIndex = 16;
            this.cbMeasure.SelectedIndexChanged += new System.EventHandler(this.cbMeasure_SelectedIndexChanged);
            // 
            // txtChunkCount
            // 
            this.txtChunkCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtChunkCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChunkCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChunkCount.Location = new System.Drawing.Point(127, 60);
            this.txtChunkCount.Name = "txtChunkCount";
            this.textboxProvider1.SetRenderTextbox(this.txtChunkCount, true);
            this.txtChunkCount.Size = new System.Drawing.Size(44, 13);
            this.textboxProvider1.SetStyle(this.txtChunkCount, XPControls.Style.Rounded);
            this.txtChunkCount.TabIndex = 18;
            this.txtChunkCount.Text = "1";
            this.txtChunkCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Stop when";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "chunk(s) have been created";
            // 
            // SplitOffChunksAndStopSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtChunkCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.cbMeasure);
            this.Name = "SplitOffChunksAndStopSettingsControl";
            this.Size = new System.Drawing.Size(353, 101);
            this.Resize += new System.EventHandler(this.SplitOffOneChunkSettingsControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtSize;
        public System.Windows.Forms.ComboBox cbMeasure;
        private XPControls.TextboxProvider textboxProvider1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtChunkCount;
        private System.Windows.Forms.Label label2;
    }
}
