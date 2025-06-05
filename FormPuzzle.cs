using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace KelimeEzberApp
{
    public partial class FormPuzzle : Form
    {
        private string correctTurkish = "";
        private int score = 0;
        private int totalQuestionsAsked = 0;
        private const int maxQuestions = 10;
        private int userId = 1;
        private bool currentQuestionScored = false;

        public FormPuzzle()
        {
            InitializeComponent();
        }

        private void FormPuzzle_Load(object sender, EventArgs e)
        {
            LoadNewWord();
            LoadPreviousScores();
        }

        private void LoadNewWord()
        {
            if (totalQuestionsAsked >= maxQuestions)
            {
                SaveScoreToDatabase();
                MessageBox.Show($"Tebrikler!\nOyun tamamlandı.\nToplam Doğru: {score} / {maxQuestions}",
                                "Kelime Testi Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit(); // Tüm uygulamayı sonlandır
                return;
            }

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = "SELECT English, Turkish FROM Words ORDER BY RANDOM() LIMIT 1";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string eng = reader["English"].ToString();
                        correctTurkish = reader["Turkish"].ToString();

                        lblEnglishWord.Text = "Kelime: " + eng;
                        txtAnswer.Text = "";
                        lblResult.Text = "";
                        lblResult.Visible = true;
                        lblResult.ForeColor = Color.Black;

                        currentQuestionScored = false;
                        totalQuestionsAsked++;
                        lblScore.Text = $"Skor: {score} / {totalQuestionsAsked}";
                    }
                }
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            string userAnswer = txtAnswer.Text.Trim();
            lblResult.Visible = true;

            if (userAnswer.Equals(correctTurkish, StringComparison.OrdinalIgnoreCase))
            {
                lblResult.Text = "✔ Doğru!";
                lblResult.ForeColor = Color.Green;

                if (!currentQuestionScored)
                {
                    score++;
                    currentQuestionScored = true;
                    lblScore.Text = $"Skor: {score} / {totalQuestionsAsked}";
                }
            }
            else
            {
                lblResult.Text = "✘ Yanlış!";
                lblResult.ForeColor = Color.Red;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            LoadNewWord();
        }

        private void SaveScoreToDatabase()
        {
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string insertQuery = @"INSERT INTO PuzzleScores (UserID, Score, TotalQuestions)
                                       VALUES (@UserID, @Score, @TotalQuestions)";
                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@Score", score);
                    cmd.Parameters.AddWithValue("@TotalQuestions", totalQuestionsAsked);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadPreviousScores()
        {
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = @"SELECT Score, TotalQuestions, FinishDate 
                                 FROM PuzzleScores 
                                 WHERE UserID = @UserID 
                                 ORDER BY FinishDate DESC";
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvScores.DataSource = dt;
                    }
                }
            }
        }
    }
}
