using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.");
                return;
            }

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p";

                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarılı!");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş olamaz.");
                return;
            }

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = "SELECT * FROM Users WHERE Username=@u AND Password=@p";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);

                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string role = reader["Role"].ToString();
                    MessageBox.Show("Giriş başarılı!");

                    if (role == "admin")
                    {
                        FormKelimeEkle formAdmin = new FormKelimeEkle();
                        formAdmin.KullaniciRol = role;
                        formAdmin.Show();
                    }
                    else
                    {
                        FormTest formOgrenci = new FormTest();
                        formOgrenci.Show();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.");
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

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = "SELECT Password FROM Users WHERE Username = @u";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@u", txtUsername.Text);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    MessageBox.Show("Şifreniz: " + result.ToString());
                }
                else
                {
                    MessageBox.Show("Bu kullanıcı adı bulunamadı.");
                }
            }
        }
    }
}
