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
using System.Drawing.Printing;

namespace KelimeEzberApp
{
    public partial class FormRapor : Form
    {
        public FormRapor()
        {
            InitializeComponent();

            // Event bağlantılarını yap (Güvenlik amaçlı burada da bağlanabilir)
            printDocument1.PrintPage += printDocument1_PrintPage;
            btnPrint.Click += btnPrint_Click;
            btnRefresh.Click += btnRefresh_Click;
        }

        private void FormRapor_Load(object sender, EventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = @"
            SELECT 
                W.Category AS Kategori,
                COUNT(*) AS Toplam,
                SUM(CASE WHEN WP.ProgressStep >= 6 THEN 1 ELSE 0 END) AS 'Öğrenilen',
                ROUND(100.0 * SUM(CASE WHEN WP.ProgressStep >= 6 THEN 1 ELSE 0 END) / COUNT(*), 1) AS 'Başarı %'
            FROM Words W
            JOIN WordProgress WP ON WP.WordId = W.Id
            WHERE WP.UserId = 1
            GROUP BY W.Category;
        ";

                using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, conn))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvReport.DataSource = table;
                }
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Önizleme penceresini göster
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = new Bitmap(dgvReport.Width, dgvReport.Height);
            dgvReport.DrawToBitmap(bmp, new Rectangle(0, 0, dgvReport.Width, dgvReport.Height));
            e.Graphics.DrawImage(bmp, 50, 50);
        }


        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReport(); // Yeniden yükle
        }

        
    }
}
