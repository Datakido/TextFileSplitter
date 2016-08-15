namespace SystemWidgets.FileSplitStrategies
{
    partial class SplitByTextBoundarySettingsControl
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
            this.grpSearchType = new System.Windows.Forms.GroupBox();
            this.rabContains = new System.Windows.Forms.RadioButton();
            this.rabLiteral = new System.Windows.Forms.RadioButton();
            this.btnResetText = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtBoundary = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkFileName = new System.Windows.Forms.CheckBox();
            this.chkOmit = new System.Windows.Forms.CheckBox();
            this.textboxProvider1 = new XPControls.TextboxProvider();
            this.epBoundary = new System.Windows.Forms.ErrorProvider(this.components);
            this.grpSearchType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epBoundary)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSearchType
            // 
            this.grpSearchType.Controls.Add(this.rabContains);
            this.grpSearchType.Controls.Add(this.rabLiteral);
            this.grpSearchType.Location = new System.Drawing.Point(15, 14);
            this.grpSearchType.Name = "grpSearchType";
            this.grpSearchType.Size = new System.Drawing.Size(86, 67);
            this.grpSearchType.TabIndex = 34;
            this.grpSearchType.TabStop = false;
            this.grpSearchType.Text = "Search Type";
            // 
            // rabContains
            // 
            this.rabContains.AutoSize = true;
            this.rabContains.Checked = true;
            this.rabContains.Location = new System.Drawing.Point(6, 20);
            this.rabContains.Name = "rabContains";
            this.rabContains.Size = new System.Drawing.Size(66, 17);
            this.rabContains.TabIndex = 12;
            this.rabContains.TabStop = true;
            this.rabContains.Text = "Contains";
            this.rabContains.UseVisualStyleBackColor = true;
            // 
            // rabLiteral
            // 
            this.rabLiteral.AutoSize = true;
            this.rabLiteral.Location = new System.Drawing.Point(6, 43);
            this.rabLiteral.Name = "rabLiteral";
            this.rabLiteral.Size = new System.Drawing.Size(53, 17);
            this.rabLiteral.TabIndex = 13;
            this.rabLiteral.TabStop = true;
            this.rabLiteral.Text = "Literal";
            this.rabLiteral.UseVisualStyleBackColor = true;
            // 
            // btnResetText
            // 
            this.btnResetText.Location = new System.Drawing.Point(362, 34);
            this.btnResetText.Name = "btnResetText";
            this.btnResetText.Size = new System.Drawing.Size(48, 20);
            this.btnResetText.TabIndex = 33;
            this.btnResetText.Text = "Reset";
            this.btnResetText.UseVisualStyleBackColor = true;
            this.btnResetText.Click += new System.EventHandler(this.btnResetText_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(117, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(142, 13);
            this.label8.TabIndex = 32;
            this.label8.Text = "Boundary Line Behavior";
            // 
            // txtBoundary
            // 
            this.txtBoundary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoundary.Location = new System.Drawing.Point(120, 80);
            this.txtBoundary.Name = "txtBoundary";
            this.textboxProvider1.SetRenderTextbox(this.txtBoundary, true);
            this.txtBoundary.Size = new System.Drawing.Size(290, 13);
            this.textboxProvider1.SetStyle(this.txtBoundary, XPControls.Style.Rounded);
            this.txtBoundary.TabIndex = 36;
            this.txtBoundary.WordWrap = false;
            this.txtBoundary.TextChanged += new System.EventHandler(this.txtBoundary_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(119, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Search for...";
            // 
            // chkFileName
            // 
            this.chkFileName.AutoSize = true;
            this.chkFileName.Location = new System.Drawing.Point(245, 35);
            this.chkFileName.Name = "chkFileName";
            this.chkFileName.Size = new System.Drawing.Size(104, 17);
            this.chkFileName.TabIndex = 39;
            this.chkFileName.Text = "Use as file name";
            this.chkFileName.UseVisualStyleBackColor = true;
            // 
            // chkOmit
            // 
            this.chkOmit.AutoSize = true;
            this.chkOmit.Location = new System.Drawing.Point(120, 35);
            this.chkOmit.Name = "chkOmit";
            this.chkOmit.Size = new System.Drawing.Size(118, 17);
            this.chkOmit.TabIndex = 38;
            this.chkOmit.Text = "Omit Boundary Line";
            this.chkOmit.UseVisualStyleBackColor = true;
            // 
            // epBoundary
            // 
            this.epBoundary.ContainerControl = this;
            // 
            // SplitByTextBoundarySettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkFileName);
            this.Controls.Add(this.chkOmit);
            this.Controls.Add(this.txtBoundary);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grpSearchType);
            this.Controls.Add(this.btnResetText);
            this.Controls.Add(this.label8);
            this.Name = "SplitByTextBoundarySettingsControl";
            this.Size = new System.Drawing.Size(447, 150);
            this.grpSearchType.ResumeLayout(false);
            this.grpSearchType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epBoundary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearchType;
        public System.Windows.Forms.RadioButton rabContains;
        public System.Windows.Forms.RadioButton rabLiteral;
        private System.Windows.Forms.Button btnResetText;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txtBoundary;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox chkFileName;
        public System.Windows.Forms.CheckBox chkOmit;
        private XPControls.TextboxProvider textboxProvider1;
        private System.Windows.Forms.ErrorProvider epBoundary;
        private System.ComponentModel.IContainer components;
    }
}
