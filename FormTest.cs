using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KelimeEzberApp
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        int currentWordId = -1;
        int userId = 1;
        string correctAnswer = "";
        Random rnd = new Random();
        private HashSet<int> todaysWords = new HashSet<int>();

        private void FormTest_Load(object sender, EventArgs e)
        {
            LoadNewQuestion();
        }

        private int GetCurrentDailyLimit()
        {
            int dailyLimit = 10;
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string q = "SELECT DailyNewWordLimit FROM Settings WHERE UserId = @u";
                SQLiteCommand cmd = new SQLiteCommand(q, conn);
                cmd.Parameters.AddWithValue("@u", userId);
                object result = cmd.ExecuteScalar();
                if (result != null)
                    dailyLimit = Convert.ToInt32(result);
            }
            return dailyLimit;
        }

        private List<int> GetTodayTestWordIds()
        {
            int dailyLimit = GetCurrentDailyLimit();
            List<int> wordIds = new List<int>();
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string query = @"
                SELECT WP.WordId, WP.ProgressStep, WP.LastCorrectDate
                FROM WordProgress WP
                WHERE WP.UserId = @u AND WP.ProgressStep < 6";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@u", userId);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int wordId = Convert.ToInt32(reader["WordId"]);
                            int step = Convert.ToInt32(reader["ProgressStep"]);
                            DateTime lastDate = Convert.ToDateTime(reader["LastCorrectDate"]);
                            int daysSinceLast = (DateTime.Now - lastDate).Days;
                            int[] requiredDelays = { 0, 1, 7, 30, 90, 180, 365 };

                            if (daysSinceLast >= requiredDelays[step])
                            {
                                string checkQuery = @"
                                SELECT COUNT(DISTINCT date(CorrectDate)) 
                                FROM WordCorrectHistory 
                                WHERE UserId = @u AND WordId = @w";
                                using (SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn))
                                {
                                    checkCmd.Parameters.AddWithValue("@u", userId);
                                    checkCmd.Parameters.AddWithValue("@w", wordId);
                                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                                    if (count < 6 && !todaysWords.Contains(wordId))
                                        wordIds.Add(wordId);
                                }
                            }
                        }
                    }
                }
            }
            return wordIds.OrderBy(x => Guid.NewGuid()).Take(dailyLimit).ToList();
        }

        private void LoadNewQuestion()
        {
            int dailyLimit = GetCurrentDailyLimit();
            var todayWordIds = GetTodayTestWordIds();

            if (todayWordIds.Count == 0 || todaysWords.Count >= dailyLimit)
            {
                MessageBox.Show("✅ Bugünkü test hakkınızı tamamladınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int randomId = todayWordIds[rnd.Next(todayWordIds.Count)];
            while (todaysWords.Contains(randomId) && todaysWords.Count < todayWordIds.Count)
                randomId = todayWordIds[rnd.Next(todayWordIds.Count)];

            if (todaysWords.Contains(randomId))
                return;

            todaysWords.Add(randomId);

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                string getWord = "SELECT * FROM Words WHERE Id = @id";
                SQLiteCommand cmd = new SQLiteCommand(getWord, conn);
                cmd.Parameters.AddWithValue("@id", randomId);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    currentWordId = Convert.ToInt32(reader["Id"]);
                    correctAnswer = reader["Turkish"].ToString();
                    lblQuestion.Text = $"'{reader["English"]}' kelimesinin Türkçesi nedir?";

                    string imgPath = reader["ImagePath"].ToString();
                    string fullPath = Path.Combine(Application.StartupPath, imgPath);
                    pbWordImage.ImageLocation = File.Exists(fullPath) ? fullPath : null;

                    reader.Close();

                    List<string> options = new List<string> { correctAnswer };
                    string getWrong = "SELECT Turkish FROM Words WHERE Id != @id ORDER BY RANDOM() LIMIT 3";
                    SQLiteCommand wrongCmd = new SQLiteCommand(getWrong, conn);
                    wrongCmd.Parameters.AddWithValue("@id", currentWordId);
                    var wrongReader = wrongCmd.ExecuteReader();
                    while (wrongReader.Read())
                        options.Add(wrongReader["Turkish"].ToString());
                    wrongReader.Close();

                    options = options.OrderBy(x => rnd.Next()).ToList();
                    if (options.Count < 4)
                    {
                        MessageBox.Show("Test için en az 4 kelime eklemelisiniz.");
                        return;
                    }

                    btnOptionA.Text = options[0];
                    btnOptionB.Text = options[1];
                    btnOptionC.Text = options[2];
                    btnOptionD.Text = options[3];
                }
            }
        }

        private void CheckAnswer(string selected)
        {
            if (selected == correctAnswer)
            {
                lblFeedback.Text = "✅ Doğru!";
                UpdateProgress(true);
            }
            else
            {
                lblFeedback.Text = "❌ Yanlış!";
                UpdateProgress(false);
            }
        }

        private void UpdateProgress(bool correct)
        {
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                string checkQuery = "SELECT * FROM WordProgress WHERE UserId=@u AND WordId=@w";
                SQLiteCommand checkCmd = new SQLiteCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@u", userId);
                checkCmd.Parameters.AddWithValue("@w", currentWordId);
                var reader = checkCmd.ExecuteReader();

                if (reader.Read())
                {
                    reader.Close();
                    string update = correct
                        ? "UPDATE WordProgress SET ProgressStep = ProgressStep + 1, LastCorrectDate = CURRENT_TIMESTAMP WHERE UserId=@u AND WordId=@w"
                        : "UPDATE WordProgress SET ProgressStep = 0, LastCorrectDate = CURRENT_TIMESTAMP WHERE UserId=@u AND WordId=@w";

                    SQLiteCommand updateCmd = new SQLiteCommand(update, conn);
                    updateCmd.Parameters.AddWithValue("@u", userId);
                    updateCmd.Parameters.AddWithValue("@w", currentWordId);
                    updateCmd.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                    string insert = "INSERT INTO WordProgress (UserId, WordId, ProgressStep, LastCorrectDate) VALUES (@u, @w, @c, CURRENT_TIMESTAMP)";
                    SQLiteCommand insertCmd = new SQLiteCommand(insert, conn);
                    insertCmd.Parameters.AddWithValue("@u", userId);
                    insertCmd.Parameters.AddWithValue("@w", currentWordId);
                    insertCmd.Parameters.AddWithValue("@c", correct ? 1 : 0);
                    insertCmd.ExecuteNonQuery();
                }

                if (correct)
                {
                    string insertDateQuery = "INSERT INTO WordCorrectHistory (UserId, WordId, CorrectDate) VALUES (@u, @w, CURRENT_DATE)";
                    SQLiteCommand cmdInsert = new SQLiteCommand(insertDateQuery, conn);
                    cmdInsert.Parameters.AddWithValue("@u", userId);
                    cmdInsert.Parameters.AddWithValue("@w", currentWordId);
                    cmdInsert.ExecuteNonQuery();
                }
            }
        }

        private void btnOptionA_Click(object sender, EventArgs e) => CheckAnswer(btnOptionA.Text);
        private void btnOptionB_Click(object sender, EventArgs e) => CheckAnswer(btnOptionB.Text);
        private void btnOptionC_Click(object sender, EventArgs e) => CheckAnswer(btnOptionC.Text);
        private void btnOptionD_Click(object sender, EventArgs e) => CheckAnswer(btnOptionD.Text);
        private void btnNext_Click(object sender, EventArgs e) { lblFeedback.Text = ""; LoadNewQuestion(); }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormSettings f = new FormSettings();
            f.ShowDialog();
            LoadNewQuestion(); // ayar değişmiş olabilir
        }
    
    private void button1_Click(object sender, EventArgs e)
        {
            // Anasayfa veya kelime ekleme ekranına dön
            FormKelimeEkle form2 = new FormKelimeEkle();
            form2.Show();
            this.Hide();
        }

        private void btnProgress_Click(object sender, EventArgs e)
        {
            // Öğrencinin ilerlemesini göster
            FormProgress form = new FormProgress();
            form.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            // Analiz raporu formunu göster
            FormRapor form = new FormRapor();
            form.ShowDialog();
        }
    }
   }
