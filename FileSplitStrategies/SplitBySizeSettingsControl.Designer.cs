namespace Datakido.FileSplitStrategies
{
    partial class SplitBySizeSettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.IContainer components;

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
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(23, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(311, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Bytes Per Chunk";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSize
            // 
            this.txtSize.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSize.Location = new System.Drawing.Point(81, 47);
            this.txtSize.Name = "txtSize";
            this.textboxProvider1.SetRenderTextbox(this.txtSize, true);
            this.txtSize.Size = new System.Drawing.Size(92, 13);
            this.textboxProvider1.SetStyle(this.txtSize, XPControls.Style.Rounded);
            this.txtSize.TabIndex = 12;
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
            this.cbMeasure.Location = new System.Drawing.Point(179, 44);
            this.cbMeasure.Name = "cbMeasure";
            this.cbMeasure.Size = new System.Drawing.Size(120, 21);
            this.textboxProvider1.SetStyle(this.cbMeasure, XPControls.Style.Rounded);
            this.cbMeasure.TabIndex = 13;
            this.cbMeasure.SelectedIndexChanged += new System.EventHandler(this.cbMeasure_SelectedIndexChanged);
            // 
            // SplitBySizeSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.cbMeasure);
            this.Name = "SplitBySizeSettingsControl";
            this.Size = new System.Drawing.Size(353, 101);
            this.Resize += new System.EventHandler(this.SplitBySizeSettingsControl_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtSize;
        public System.Windows.Forms.ComboBox cbMeasure;
        private XPControls.TextboxProvider textboxProvider1;
    }
}
