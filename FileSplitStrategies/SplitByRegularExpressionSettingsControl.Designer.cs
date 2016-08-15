namespace SystemWidgets.FileSplitStrategies
{
    partial class SplitByRegularExpressionSettingsControl
    {

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
            this.btnResetRegex = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRegex = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkOmit = new System.Windows.Forms.CheckBox();
            this.chkFileName = new System.Windows.Forms.CheckBox();
            this.textboxProvider1 = new XPControls.TextboxProvider();
            this.epRegex = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epRegex)).BeginInit();
            this.SuspendLayout();
            // 
            // btnResetRegex
            // 
            this.btnResetRegex.Location = new System.Drawing.Point(316, 30);
            this.btnResetRegex.Name = "btnResetRegex";
            this.btnResetRegex.Size = new System.Drawing.Size(48, 20);
            this.btnResetRegex.TabIndex = 33;
            this.btnResetRegex.Text = "Reset";
            this.btnResetRegex.UseVisualStyleBackColor = true;
            this.btnResetRegex.Click += new System.EventHandler(this.btnResetRegex_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(75, 14);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(142, 13);
            this.label11.TabIndex = 32;
            this.label11.Text = "Boundary Line Behavior";
            // 
            // txtRegex
            // 
            this.txtRegex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRegex.Location = new System.Drawing.Point(75, 77);
            this.txtRegex.Name = "txtRegex";
            this.textboxProvider1.SetRenderTextbox(this.txtRegex, true);
            this.txtRegex.Size = new System.Drawing.Size(286, 13);
            this.textboxProvider1.SetStyle(this.txtRegex, XPControls.Style.Rounded);
            this.txtRegex.TabIndex = 35;
            this.txtRegex.WordWrap = false;
            this.txtRegex.TextChanged += new System.EventHandler(this.txtRegex_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(72, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(191, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Regular Expression for boundary";
            // 
            // chkOmit
            // 
            this.chkOmit.AutoSize = true;
            this.chkOmit.Location = new System.Drawing.Point(78, 32);
            this.chkOmit.Name = "chkOmit";
            this.chkOmit.Size = new System.Drawing.Size(118, 17);
            this.chkOmit.TabIndex = 36;
            this.chkOmit.Text = "Omit Boundary Line";
            this.chkOmit.UseVisualStyleBackColor = true;
            // 
            // chkFileName
            // 
            this.chkFileName.AutoSize = true;
            this.chkFileName.Location = new System.Drawing.Point(203, 32);
            this.chkFileName.Name = "chkFileName";
            this.chkFileName.Size = new System.Drawing.Size(104, 17);
            this.chkFileName.TabIndex = 37;
            this.chkFileName.Text = "Use as file name";
            this.chkFileName.UseVisualStyleBackColor = true;
            // 
            // epRegex
            // 
            this.epRegex.ContainerControl = this;
            // 
            // SplitByRegularExpressionSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkFileName);
            this.Controls.Add(this.chkOmit);
            this.Controls.Add(this.txtRegex);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnResetRegex);
            this.Controls.Add(this.label11);
            this.Name = "SplitByRegularExpressionSettingsControl";
            this.Size = new System.Drawing.Size(432, 150);
            ((System.ComponentModel.ISupportInitialize)(this.epRegex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnResetRegex;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox txtRegex;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.CheckBox chkOmit;
        public System.Windows.Forms.CheckBox chkFileName;
        private XPControls.TextboxProvider textboxProvider1;
        private System.Windows.Forms.ErrorProvider epRegex;
        private System.ComponentModel.IContainer components;
    }
}
