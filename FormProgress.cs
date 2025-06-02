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
    public partial class FormProgress : Form
    {   
        public FormProgress()
        {
            InitializeComponent();
        }

        private void FormProgress_Load(object sender, EventArgs e)
        {
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT 
                W.English,
                W.Turkish,
                WP.CorrectCount,
                CASE 
                    WHEN WP.CorrectCount >= 6 THEN '✔️ Öğrenildi' 
                    ELSE '⏳ Devam ediyor' 
                END AS Status
            FROM WordProgress WP
            INNER JOIN Words W ON WP.WordId = W.Id
            WHERE WP.UserId = 1";

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvProgress.DataSource = table;
            }
        }
    }
}
