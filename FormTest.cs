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
        private int totalQuestionsAsked = 0;
        private bool testFinished = false;
        private List<int> cachedTodayWordIds;

        private void FormTest_Load(object sender, EventArgs e)
        {
            cachedTodayWordIds = GetTodayTestWordIds();
            LoadNewQuestion();
        }

        private int GetCurrentDailyLimit()
        {
            int dailyLimit = 10;
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT DailyNewWordLimit FROM Settings WHERE UserId = @u", conn))
                {
                    cmd.Parameters.AddWithValue("@u", userId);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        dailyLimit = Convert.ToInt32(result);
                }
            }
            return dailyLimit;
        }

        private List<int> GetTodayTestWordIds()
        {
            int dailyLimit = GetCurrentDailyLimit();
            HashSet<int> selectedWordIds = new HashSet<int>();
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";

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
                        while (reader.Read() && selectedWordIds.Count < dailyLimit)
                        {
                            int wordId = Convert.ToInt32(reader["WordId"]);
                            int step = Convert.ToInt32(reader["ProgressStep"]);
                            DateTime lastDate = Convert.ToDateTime(reader["LastCorrectDate"]);
                            int daysSinceLast = (DateTime.Now - lastDate).Days;
                            int[] requiredDelays = { 0, 1, 7, 30, 90, 180, 365 };

                            if (daysSinceLast >= requiredDelays[step])
                            {
                                using (SQLiteCommand checkCmd = new SQLiteCommand(@"
                                SELECT COUNT(DISTINCT date(CorrectDate)) 
                                FROM WordCorrectHistory 
                                WHERE UserId = @u AND WordId = @w", conn))
                                {
                                    checkCmd.Parameters.AddWithValue("@u", userId);
                                    checkCmd.Parameters.AddWithValue("@w", wordId);
                                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                                    if (count < 6 && !selectedWordIds.Contains(wordId))
                                        selectedWordIds.Add(wordId);
                                }
                            }
                        }
                    }
                }

                if (selectedWordIds.Count < dailyLimit)
                {
                    string fillQuery = @"
                    SELECT Id FROM Words 
                    WHERE Id NOT IN (" + string.Join(",", selectedWordIds.DefaultIfEmpty(-1)) + @")
                    ORDER BY RANDOM() LIMIT @kalan";

                    using (SQLiteCommand cmd = new SQLiteCommand(fillQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@kalan", dailyLimit - selectedWordIds.Count);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["Id"]);
                                selectedWordIds.Add(id);
                            }
                        }
                    }
                }
            }

            return selectedWordIds.OrderBy(x => Guid.NewGuid()).ToList();
        }

        private void LoadNewQuestion()
        {
            if (testFinished) return;

            int dailyLimit = GetCurrentDailyLimit();

            if (cachedTodayWordIds.Count == 0 || todaysWords.Count >= dailyLimit || totalQuestionsAsked >= dailyLimit)
            {
                MessageBox.Show("✅ Bugünkü test hakkınızı tamamladınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                testFinished = true;
                return;
            }

            int randomId = cachedTodayWordIds[rnd.Next(cachedTodayWordIds.Count)];
            while (todaysWords.Contains(randomId) && todaysWords.Count < cachedTodayWordIds.Count)
                randomId = cachedTodayWordIds[rnd.Next(cachedTodayWordIds.Count)];

            if (todaysWords.Contains(randomId)) return;
            todaysWords.Add(randomId);
            totalQuestionsAsked++;

            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM Words WHERE Id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", randomId);
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentWordId = Convert.ToInt32(reader["Id"]);
                            correctAnswer = reader["Turkish"].ToString();
                            lblQuestion.Text = $"'{reader["English"]}' kelimesinin Türkçesi nedir?";

                            string imgPath = reader["ImagePath"].ToString();
                            string fullPath = Path.Combine(Application.StartupPath, imgPath);
                            pbWordImage.ImageLocation = File.Exists(fullPath) ? fullPath : null;

                            List<string> options = new List<string> { correctAnswer };
                            using (SQLiteCommand wrongCmd = new SQLiteCommand("SELECT Turkish FROM Words WHERE Id != @id ORDER BY RANDOM() LIMIT 3", conn))
                            {
                                wrongCmd.Parameters.AddWithValue("@id", currentWordId);
                                using (SQLiteDataReader wrongReader = wrongCmd.ExecuteReader())
                                {
                                    while (wrongReader.Read())
                                        options.Add(wrongReader["Turkish"].ToString());
                                }
                            }

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
            }
        }

        private void CheckAnswer(string selected)
        {
            if (testFinished) return;

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
            string connStr = $"Data Source={Application.StartupPath}\\kelime.db;Version=3;BusyTimeout=5000;";
            using (SQLiteConnection conn = new SQLiteConnection(connStr))
            {
                conn.Open();

                using (SQLiteCommand checkCmd = new SQLiteCommand("SELECT * FROM WordProgress WHERE UserId=@u AND WordId=@w", conn))
                {
                    checkCmd.Parameters.AddWithValue("@u", userId);
                    checkCmd.Parameters.AddWithValue("@w", currentWordId);

                    using (SQLiteDataReader reader = checkCmd.ExecuteReader())
                    {
                        bool hasRow = reader.Read();
                        reader.Close();

                        if (hasRow)
                        {
                            string update = correct
                                ? "UPDATE WordProgress SET ProgressStep = ProgressStep + 1, CorrectCount = CorrectCount + 1, LastCorrectDate = CURRENT_TIMESTAMP WHERE UserId=@u AND WordId=@w"
                                : "UPDATE WordProgress SET ProgressStep = 0, LastCorrectDate = CURRENT_TIMESTAMP WHERE UserId=@u AND WordId=@w";

                            using (SQLiteCommand updateCmd = new SQLiteCommand(update, conn))
                            {
                                updateCmd.Parameters.AddWithValue("@u", userId);
                                updateCmd.Parameters.AddWithValue("@w", currentWordId);
                                updateCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insert = "INSERT INTO WordProgress (UserId, WordId, ProgressStep, CorrectCount, LastCorrectDate) VALUES (@u, @w, @c, @cc, CURRENT_TIMESTAMP)";
                            using (SQLiteCommand insertCmd = new SQLiteCommand(insert, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@u", userId);
                                insertCmd.Parameters.AddWithValue("@w", currentWordId);
                                insertCmd.Parameters.AddWithValue("@c", correct ? 1 : 0);
                                insertCmd.Parameters.AddWithValue("@cc", correct ? 1 : 0);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }

                if (correct)                                                                                                                                                                                
                {
                    using (SQLiteCommand cmdInsert = new SQLiteCommand("INSERT INTO WordCorrectHistory (UserId, WordId, CorrectDate) VALUES (@u, @w, CURRENT_DATE)", conn))
                    {
                        cmdInsert.Parameters.AddWithValue("@u", userId);
                        cmdInsert.Parameters.AddWithValue("@w", currentWordId);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
            }
        }

        private void btnOptionA_Click(object sender, EventArgs e) => CheckAnswer(btnOptionA.Text);
        private void btnOptionB_Click(object sender, EventArgs e) => CheckAnswer(btnOptionB.Text);
        private void btnOptionC_Click(object sender, EventArgs e) => CheckAnswer(btnOptionC.Text);
        private void btnOptionD_Click(object sender, EventArgs e) => CheckAnswer(btnOptionD.Text);

        private void btnNext_Click(object sender, EventArgs e)
        {
            lblFeedback.Text = "";
            LoadNewQuestion();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            FormSettings f = new FormSettings();
            f.ShowDialog();
            todaysWords.Clear();
            totalQuestionsAsked = 0;
            testFinished = false;
            cachedTodayWordIds = GetTodayTestWordIds();
            LoadNewQuestion();
        }

        private void btnProgress_Click(object sender, EventArgs e)
        {
            FormProgress form = new FormProgress();
            form.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            FormRapor form = new FormRapor();
            form.ShowDialog();
        }

        public void TestMetodu()
        {
            Console.WriteLine("Test metodu çalıştı.");
        }
    }
}
