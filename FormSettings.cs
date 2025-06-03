using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace KelimeEzberApp
{
    public partial class FormSettings : Form
    {
        int userId = 1;

        public FormSettings()
        {
            InitializeComponent();
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            try
            {
                string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();

                    string insertIfNotExists = "INSERT OR IGNORE INTO Settings (UserId, DailyNewWordLimit) VALUES (@u, 10)";
                    SQLiteCommand insertCmd = new SQLiteCommand(insertIfNotExists, conn);
                    insertCmd.Parameters.AddWithValue("@u", userId);
                    insertCmd.ExecuteNonQuery();

                    string query = "SELECT DailyNewWordLimit FROM Settings WHERE UserId = @u";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@u", userId);
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        nudNewWordLimit1.Value = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ayarlar yüklenirken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
                using (SQLiteConnection conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string update = "UPDATE Settings SET DailyNewWordLimit = @limit WHERE UserId = @u";
                    SQLiteCommand cmd = new SQLiteCommand(update, conn);
                    cmd.Parameters.AddWithValue("@limit", nudNewWordLimit1.Value);
                    cmd.Parameters.AddWithValue("@u", userId);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Ayarlar kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydederken hata oluştu:\n" + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
