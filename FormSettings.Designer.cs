namespace KelimeEzberApp
{
    partial class FormSettings
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
            this.lblLimitInfo = new System.Windows.Forms.Label();
            this.nudNewWordLimit1 = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudNewWordLimit1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLimitInfo
            // 
            this.lblLimitInfo.AutoSize = true;
            this.lblLimitInfo.Location = new System.Drawing.Point(191, 148);
            this.lblLimitInfo.Name = "lblLimitInfo";
            this.lblLimitInfo.Size = new System.Drawing.Size(132, 13);
            this.lblLimitInfo.TabIndex = 0;
            this.lblLimitInfo.Text = "Günlük Yeni Kelime Sayısı:";
            // 
            // nudNewWordLimit1
            // 
            this.nudNewWordLimit1.Location = new System.Drawing.Point(329, 146);
            this.nudNewWordLimit1.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudNewWordLimit1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNewWordLimit1.Name = "nudNewWordLimit1";
            this.nudNewWordLimit1.Size = new System.Drawing.Size(120, 20);
            this.nudNewWordLimit1.TabIndex = 1;
            this.nudNewWordLimit1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(455, 143);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.nudNewWordLimit1);
            this.Controls.Add(this.lblLimitInfo);
            this.Name = "FormSettings";
            ((System.ComponentModel.ISupportInitialize)(this.nudNewWordLimit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLimitInfo;
        private System.Windows.Forms.NumericUpDown nudNewWordLimit1;
        private System.Windows.Forms.Button btnSave;
    }
}