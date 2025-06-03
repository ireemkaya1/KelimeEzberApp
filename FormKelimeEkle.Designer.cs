namespace KelimeEzberApp
{
    partial class FormKelimeEkle
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
            this.lblEnglish = new System.Windows.Forms.Label();
            this.txtEnglish = new System.Windows.Forms.TextBox();
            this.lblTurkish = new System.Windows.Forms.Label();
            this.txtTurkish = new System.Windows.Forms.TextBox();
            this.lblSentence1 = new System.Windows.Forms.Label();
            this.txtSentence1 = new System.Windows.Forms.TextBox();
            this.lblSentence2 = new System.Windows.Forms.Label();
            this.txtSentence2 = new System.Windows.Forms.TextBox();
            this.lblSentence3 = new System.Windows.Forms.Label();
            this.txtSentence3 = new System.Windows.Forms.TextBox();
            this.lblImage = new System.Windows.Forms.Label();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.btnSaveWord = new System.Windows.Forms.Button();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblEnglish
            // 
            this.lblEnglish.AutoSize = true;
            this.lblEnglish.Location = new System.Drawing.Point(139, 37);
            this.lblEnglish.Name = "lblEnglish";
            this.lblEnglish.Size = new System.Drawing.Size(79, 13);
            this.lblEnglish.TabIndex = 0;
            this.lblEnglish.Text = "İngilizce Kelime";
            // 
            // txtEnglish
            // 
            this.txtEnglish.Location = new System.Drawing.Point(224, 34);
            this.txtEnglish.Name = "txtEnglish";
            this.txtEnglish.Size = new System.Drawing.Size(100, 20);
            this.txtEnglish.TabIndex = 1;
            // 
            // lblTurkish
            // 
            this.lblTurkish.AutoSize = true;
            this.lblTurkish.Location = new System.Drawing.Point(139, 77);
            this.lblTurkish.Name = "lblTurkish";
            this.lblTurkish.Size = new System.Drawing.Size(75, 13);
            this.lblTurkish.TabIndex = 0;
            this.lblTurkish.Text = "Türkçe Anlamı\n";
            // 
            // txtTurkish
            // 
            this.txtTurkish.Location = new System.Drawing.Point(224, 74);
            this.txtTurkish.Name = "txtTurkish";
            this.txtTurkish.Size = new System.Drawing.Size(100, 20);
            this.txtTurkish.TabIndex = 1;
            // 
            // lblSentence1
            // 
            this.lblSentence1.AutoSize = true;
            this.lblSentence1.Location = new System.Drawing.Point(150, 114);
            this.lblSentence1.Name = "lblSentence1";
            this.lblSentence1.Size = new System.Drawing.Size(45, 13);
            this.lblSentence1.TabIndex = 0;
            this.lblSentence1.Text = "Cümle 1\n";
            // 
            // txtSentence1
            // 
            this.txtSentence1.Location = new System.Drawing.Point(223, 111);
            this.txtSentence1.Name = "txtSentence1";
            this.txtSentence1.Size = new System.Drawing.Size(100, 20);
            this.txtSentence1.TabIndex = 1;
            // 
            // lblSentence2
            // 
            this.lblSentence2.AutoSize = true;
            this.lblSentence2.Location = new System.Drawing.Point(150, 140);
            this.lblSentence2.Name = "lblSentence2";
            this.lblSentence2.Size = new System.Drawing.Size(45, 13);
            this.lblSentence2.TabIndex = 0;
            this.lblSentence2.Text = "Cümle 2";
            // 
            // txtSentence2
            // 
            this.txtSentence2.Location = new System.Drawing.Point(223, 137);
            this.txtSentence2.Name = "txtSentence2";
            this.txtSentence2.Size = new System.Drawing.Size(100, 20);
            this.txtSentence2.TabIndex = 1;
            // 
            // lblSentence3
            // 
            this.lblSentence3.AutoSize = true;
            this.lblSentence3.Location = new System.Drawing.Point(150, 166);
            this.lblSentence3.Name = "lblSentence3";
            this.lblSentence3.Size = new System.Drawing.Size(45, 13);
            this.lblSentence3.TabIndex = 0;
            this.lblSentence3.Text = "Cümle 3";
            // 
            // txtSentence3
            // 
            this.txtSentence3.Location = new System.Drawing.Point(223, 163);
            this.txtSentence3.Name = "txtSentence3";
            this.txtSentence3.Size = new System.Drawing.Size(100, 20);
            this.txtSentence3.TabIndex = 1;
            // 
            // lblImage
            // 
            this.lblImage.AutoSize = true;
            this.lblImage.Location = new System.Drawing.Point(150, 219);
            this.lblImage.Name = "lblImage";
            this.lblImage.Size = new System.Drawing.Size(59, 13);
            this.lblImage.TabIndex = 2;
            this.lblImage.Text = "Görsel Seç";
            // 
            // pbImage
            // 
            this.pbImage.Location = new System.Drawing.Point(223, 201);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(100, 50);
            this.pbImage.TabIndex = 3;
            this.pbImage.TabStop = false;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(329, 214);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(75, 23);
            this.btnSelectImage.TabIndex = 4;
            this.btnSelectImage.Text = "Resim Seç";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // btnSaveWord
            // 
            this.btnSaveWord.Location = new System.Drawing.Point(221, 309);
            this.btnSaveWord.Name = "btnSaveWord";
            this.btnSaveWord.Size = new System.Drawing.Size(102, 23);
            this.btnSaveWord.TabIndex = 4;
            this.btnSaveWord.Text = "Kaydet";
            this.btnSaveWord.UseVisualStyleBackColor = true;
            this.btnSaveWord.Click += new System.EventHandler(this.btnSaveWord_Click);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(205, 267);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(121, 21);
            this.cmbCategory.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(150, 270);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Kategori:";
            // 
            // FormKelimeEkle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(782, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.btnSaveWord);
            this.Controls.Add(this.btnSelectImage);
            this.Controls.Add(this.pbImage);
            this.Controls.Add(this.lblImage);
            this.Controls.Add(this.txtSentence3);
            this.Controls.Add(this.txtSentence2);
            this.Controls.Add(this.lblSentence3);
            this.Controls.Add(this.txtSentence1);
            this.Controls.Add(this.lblSentence2);
            this.Controls.Add(this.txtTurkish);
            this.Controls.Add(this.lblSentence1);
            this.Controls.Add(this.lblTurkish);
            this.Controls.Add(this.txtEnglish);
            this.Controls.Add(this.lblEnglish);
            this.Name = "FormKelimeEkle";
            this.Text = "FormKelimeEkle";
            this.Load += new System.EventHandler(this.FormKelimeEkle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEnglish;
        private System.Windows.Forms.TextBox txtEnglish;
        private System.Windows.Forms.Label lblTurkish;
        private System.Windows.Forms.TextBox txtTurkish;
        private System.Windows.Forms.Label lblSentence1;
        private System.Windows.Forms.TextBox txtSentence1;
        private System.Windows.Forms.Label lblSentence2;
        private System.Windows.Forms.TextBox txtSentence2;
        private System.Windows.Forms.Label lblSentence3;
        private System.Windows.Forms.TextBox txtSentence3;
        private System.Windows.Forms.Label lblImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.Button btnSaveWord;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label1;
    }
}