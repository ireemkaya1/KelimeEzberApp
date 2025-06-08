using System;
using System.Data.SQLite;

using System.Windows.Forms;

namespace KelimeEzberApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string kullanici = txtUsername.Text.Trim();
            string sifre = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(kullanici) || string.IsNullOrWhiteSpace(sifre))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.");
                return;
            }

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", kullanici);
                    cmd.Parameters.AddWithValue("@p", sifre);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("Giriş başarılı!");

                            this.Hide();

                            if (kullanici == "admin")
                            {
                                FormKelimeEkle adminForm = new FormKelimeEkle();
                                adminForm.KullaniciRol = "admin"; // İstersen kaldır
                                adminForm.ShowDialog();
                            }
                            else
                            {
                                FormTest testForm = new FormTest();
                                testForm.ShowDialog();
                            }

                            this.Show();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
                        }
                    }
                }
            }
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.");
                return;
            }

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string kontrolQuery = "SELECT COUNT(*) FROM Users WHERE Username=@u";
                using (var kontrolCmd = new SQLiteCommand(kontrolQuery, conn))
                {
                    kontrolCmd.Parameters.AddWithValue("@u", txtUsername.Text);
                    long count = (long)kontrolCmd.ExecuteScalar();
                    if (count > 0)
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten alınmış.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, 'user')";
                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@p", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarılı!");
                }
            }
        }

        private void btnForgotPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adınızı girin.");
                return;
            }

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Password FROM Users WHERE Username = @u";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        MessageBox.Show("Şifreniz: " + result.ToString());
                    else
                        MessageBox.Show("Bu kullanıcı adı bulunamadı.");
                }
            }
        }
    }
} 
