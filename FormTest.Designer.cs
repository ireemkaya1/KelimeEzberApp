namespace KelimeEzberApp
{
    partial class FormTest
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
            this.lblQuestion = new System.Windows.Forms.Label();
            this.btnOptionA = new System.Windows.Forms.Button();
            this.btnOptionB = new System.Windows.Forms.Button();
            this.btnOptionC = new System.Windows.Forms.Button();
            this.btnOptionD = new System.Windows.Forms.Button();
            this.lblFeedback = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pbWordImage = new System.Windows.Forms.PictureBox();
            this.btnProgress = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbWordImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.AutoSize = true;
            this.lblQuestion.Location = new System.Drawing.Point(102, 93);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(0, 13);
            this.lblQuestion.TabIndex = 0;
            // 
            // btnOptionA
            // 
            this.btnOptionA.Location = new System.Drawing.Point(95, 149);
            this.btnOptionA.Name = "btnOptionA";
            this.btnOptionA.Size = new System.Drawing.Size(75, 23);
            this.btnOptionA.TabIndex = 1;
            this.btnOptionA.Text = "A şıkkı";
            this.btnOptionA.UseVisualStyleBackColor = true;
            this.btnOptionA.Click += new System.EventHandler(this.btnOptionA_Click);
            // 
            // btnOptionB
            // 
            this.btnOptionB.Location = new System.Drawing.Point(193, 149);
            this.btnOptionB.Name = "btnOptionB";
            this.btnOptionB.Size = new System.Drawing.Size(75, 23);
            this.btnOptionB.TabIndex = 1;
            this.btnOptionB.Text = "B şıkkı";
            this.btnOptionB.UseVisualStyleBackColor = true;
            this.btnOptionB.Click += new System.EventHandler(this.btnOptionB_Click);
            // 
            // btnOptionC
            // 
            this.btnOptionC.Location = new System.Drawing.Point(95, 178);
            this.btnOptionC.Name = "btnOptionC";
            this.btnOptionC.Size = new System.Drawing.Size(75, 23);
            this.btnOptionC.TabIndex = 1;
            this.btnOptionC.Text = "C şıkkı";
            this.btnOptionC.UseVisualStyleBackColor = true;
            this.btnOptionC.Click += new System.EventHandler(this.btnOptionC_Click);
            // 
            // btnOptionD
            // 
            this.btnOptionD.Location = new System.Drawing.Point(193, 178);
            this.btnOptionD.Name = "btnOptionD";
            this.btnOptionD.Size = new System.Drawing.Size(75, 23);
            this.btnOptionD.TabIndex = 1;
            this.btnOptionD.Text = "D şıkkı";
            this.btnOptionD.UseVisualStyleBackColor = true;
            this.btnOptionD.Click += new System.EventHandler(this.btnOptionD_Click);
            // 
            // lblFeedback
            // 
            this.lblFeedback.AutoSize = true;
            this.lblFeedback.Location = new System.Drawing.Point(102, 253);
            this.lblFeedback.Name = "lblFeedback";
            this.lblFeedback.Size = new System.Drawing.Size(0, 13);
            this.lblFeedback.TabIndex = 0;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(396, 296);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(76, 36);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Sonraki Soru\t";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 296);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Geri Dön\t";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbWordImage
            // 
            this.pbWordImage.Location = new System.Drawing.Point(342, 93);
            this.pbWordImage.Name = "pbWordImage";
            this.pbWordImage.Size = new System.Drawing.Size(100, 50);
            this.pbWordImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbWordImage.TabIndex = 2;
            this.pbWordImage.TabStop = false;
            // 
            // btnProgress
            // 
            this.btnProgress.Location = new System.Drawing.Point(337, 351);
            this.btnProgress.Name = "btnProgress";
            this.btnProgress.Size = new System.Drawing.Size(106, 36);
            this.btnProgress.TabIndex = 1;
            this.btnProgress.Text = "İlerlemeyi Göster";
            this.btnProgress.UseVisualStyleBackColor = true;
            this.btnProgress.Click += new System.EventHandler(this.btnProgress_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(575, 12);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(47, 43);
            this.btnSettings.TabIndex = 1;
            this.btnSettings.Text = "Ayarlar ⚙️\n";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(449, 351);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(110, 36);
            this.btnReport.TabIndex = 1;
            this.btnReport.Text = "📊 Rapor";
            this.btnReport.UseVisualStyleBackColor = true;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // FormTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 405);
            this.Controls.Add(this.pbWordImage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnReport);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnProgress);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnOptionD);
            this.Controls.Add(this.btnOptionC);
            this.Controls.Add(this.btnOptionB);
            this.Controls.Add(this.btnOptionA);
            this.Controls.Add(this.lblFeedback);
            this.Controls.Add(this.lblQuestion);
            this.Name = "FormTest";
            this.Text = "FormTest";
            this.Load += new System.EventHandler(this.FormTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbWordImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.Button btnOptionA;
        private System.Windows.Forms.Button btnOptionB;
        private System.Windows.Forms.Button btnOptionC;
        private System.Windows.Forms.Button btnOptionD;
        private System.Windows.Forms.Label lblFeedback;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pbWordImage;
        private System.Windows.Forms.Button btnProgress;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnReport;
    }
}