﻿using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace KelimeEzberApp
{
    public partial class FormKelimeEkle : Form
    {
        public FormKelimeEkle()
        {
            InitializeComponent();
        }

        public string KullaniciRol { get; set; }

        private void FormKelimeEkle_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.AddRange(new string[] { "Hayvanlar", "Fiiller", "Yiyecekler", "Teknoloji", "Doğa", "Genel" });
            cmbCategory.SelectedIndex = 0;

           
        }

        private void btnSaveWord_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEnglish.Text) || string.IsNullOrWhiteSpace(txtTurkish.Text))
            {
                MessageBox.Show("Lütfen İngilizce ve Türkçe alanlarını doldurun.");
                return;
            }

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";
            string imagePath = pbImage.ImageLocation != null ? Path.GetFileName(pbImage.ImageLocation) : "";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();

                    string query = @"INSERT INTO Words 
                                     (English, Turkish, Sentence1, Sentence2, Sentence3, ImagePath, Category) 
                                     VALUES (@e, @t, @s1, @s2, @s3, @img, @cat)";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@e", txtEnglish.Text.Trim());
                        cmd.Parameters.AddWithValue("@t", txtTurkish.Text.Trim());
                        cmd.Parameters.AddWithValue("@s1", txtSentence1.Text.Trim());
                        cmd.Parameters.AddWithValue("@s2", txtSentence2.Text.Trim());
                        cmd.Parameters.AddWithValue("@s3", txtSentence3.Text.Trim());
                        cmd.Parameters.AddWithValue("@img", imagePath);
                        cmd.Parameters.AddWithValue("@cat", cmbCategory.SelectedItem?.ToString() ?? "Genel");

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Kelime başarıyla kaydedildi!");

                // Form alanlarını temizle
                txtEnglish.Clear();
                txtTurkish.Clear();
                txtSentence1.Clear();
                txtSentence2.Clear();
                txtSentence3.Clear();
                pbImage.ImageLocation = null;
                cmbCategory.SelectedIndex = 0;
            }
            catch (SQLiteException sqlEx) when (sqlEx.Message.Contains("database is locked"))
            {
                MessageBox.Show("HATA: Veritabanı şu anda meşgul. Lütfen birkaç saniye sonra tekrar deneyin.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA: " + ex.Message);
            }
        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Resim Dosyaları|*.jpg;*.png;*.jpeg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pbImage.ImageLocation = ofd.FileName;
                }
            }
        }
    }
}
