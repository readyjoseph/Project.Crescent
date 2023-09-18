using OpenAI_API;
using OpenAI_API.Chat;
using System.Data;
using OpenAI_API.Completions;
using System.Configuration;
using Crescent.WinForms.Model;
using System.Windows.Forms.DataVisualization.Charting;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.VisualStyles;

namespace Crescent.WinForms
{
    public partial class DataLoader_v2 : Form
    {
        #region Variables
        private string apiKey = string.Empty;
        private string organisation = "org-BtV4Ff7q0LPWT1KgS2Q1BWb8";
        private static OpenAIAPI api;
        private static ChatEndpoint chatEndpoint;
        private static Conversation conversation;
        //public ChatResult chat;
        private static string filePath;
        private static string fileName => (filePath?.Length > 0 ? System.IO.Path.GetFileName(filePath) : "");
        private static Font fontHeader => new Font("Segoe UI", 9, FontStyle.Bold);

        //private static string jsonFilePath => System.IO.Path.GetDirectoryName(filePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(filePath) + ".jsonl";
        //private static string jsonFileName => System.IO.Path.GetFileName(jsonFilePath);
        private static string reportsList;
        private static bool loadedFile = false;
        private static bool loadedReports = false;
        #endregion

        #region Constructor
        public DataLoader_v2()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void DataLoader_Load(object sender, EventArgs e)
        {
            //cmbAnalysisType.Items.Clear();
            //cmbAnalysisType.Items.Add("Fixed Questions");
            //cmbAnalysisType.Items.Add("Free Text");

            //cmbAnalysisType.SelectedIndex = 0;
            //cmbAnalysisType_SelectedIndexChanged(cmbAnalysisType, new EventArgs());

            //cmbFixedQuestions.Items.Clear();
            //cmbFixedQuestions.Items.Add("Trend questions");
            //cmbFixedQuestions.Items.Add("Grouped queries");
            //cmbFixedQuestions.Items.Add("Summary questions");
            //cmbFixedQuestions.SelectedIndex = 0;

            //cmbShowResultsAs.Items.Clear();
            //cmbShowResultsAs.Items.Add("Text");
            //cmbShowResultsAs.Items.Add("Chart");
            //cmbShowResultsAs.SelectedIndex = 1;

            apiKey = ConfigurationManager.AppSettings["OpenAI.APIKey"].ToString();
            organisation = ConfigurationManager.AppSettings["OpenAI.OrganisationId"].ToString();
            reportsList = ConfigurationManager.AppSettings["TestData.ReportsPath"].ToString();

            api = new OpenAIAPI(apiKey);

            lblResult.Visible = false;
            txtResult.Visible = false;
            chartControl.Visible = false;
        }
        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Text Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "txt",
                Filter = "Text files (*.txt)|*.txt|CSV files (*.csv)|*.CSV",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textFileSelector.Text = openFileDialog1.FileName;
                // Call a method to read and load data from the selected file into the DataGridView
                LoadDataIntoDataGridView(openFileDialog1.FileName);
                filePath = openFileDialog1.FileName;

                lblResult.Visible = false;
                txtResult.Visible = false;
                chartControl.Visible = false;
            }
        }

        private void btnGetQuestions_Click(object sender, EventArgs e)
        {
            List<Question> questions = null;
            string answer = string.Empty;
            loadedFile = false;

            questions = GetQuestions("Give me questions I can plot on a chart, and then under the heading Related Reports with restarted numbering list my reports which closely match the input data. Don't give me any footnotes or explanations").Result;

            panel1.Controls.Clear();

            if (questions?.Count > 0)
            {
                int top = 0;
                bool relatedReport = false;

                Label header = new Label();
                header.Text = $"Questions";
                header.Left = 20;
                header.Top = top;
                header.Width = panel1.Width - 50;
                header.Font = fontHeader;
                top += 20;
                panel1.Controls.Add(header);

                foreach (var question in questions)
                {
                    if (question.GroupName.Contains("Reports") && question.QuestionIndex == 1)
                    {
                        relatedReport = true;
                        header = new Label();
                        header.Text = $"{question.GroupName}";
                        header.Left = 20;
                        header.Top = top;
                        header.Width = panel1.Width - 50;
                        header.Font = fontHeader;
                        top += 20;
                        panel1.Controls.Add(header);                        
                    }

                    if (!relatedReport)
                    {
                        LinkLabel btn = new LinkLabel();
                        btn.Text = $"{question.QuestionIndex} {question.QuestionName}";
                        btn.Tag = "Question:" + question.QuestionIndex;
                        btn.Left = 20;
                        btn.Top = top;
                        btn.Width = panel1.Width - 50;
                        top += 20;
                        btn.Click += (sender, e) =>
                        {
                            string[] tagsParts = Convert.ToString((sender as LinkLabel).Tag).Split(new string[] { ":"}, StringSplitOptions.RemoveEmptyEntries);
                            if (tagsParts?.Length > 1)
                            {
                                answer = GetQuestionResponse(Convert.ToByte(tagsParts[1]), string.Empty, "Chart").Result;
                                lblResult.Text = $"Result - {question.QuestionName}";
                                DisplayResults(answer);
                            }
                        };
                        panel1.Controls.Add(btn);
                    }
                    else
                    {
                        LinkLabel btn = new LinkLabel();
                        btn.Text = $"{question.QuestionIndex} {question.QuestionName}";
                        btn.Tag = "Report:" + question.QuestionIndex;
                        btn.Left = 20;
                        btn.Top = top;
                        btn.Width = panel1.Width - 50;
                        top += 20;
                        btn.Click += (sender, e) =>
                        {
                            //string[] tagsParts = Convert.ToString((sender as LinkLabel).Tag).Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                            //if (tagsParts?.Length > 1)
                            //{
                            //    answer = GetQuestionResponse(Convert.ToByte(tagsParts[1]), string.Empty, "Chart").Result;
                            //    DisplayResults(answer);
                            //}
                        };
                        panel1.Controls.Add(btn);
                    }
                }
                panel1.AutoScroll = true;
            }
        }
        private void DisplayResults(string answer)
        {
            lblResult.Visible = true;
            txtResult.Text = "";
            chartControl.Series.Clear();
            ParseResultToChart(answer);
        }
        #endregion

        #region Private Methods
        //Sample main question: Give me questions I can plot on a chart
        //Sample individual question: Give me the answer for question 1 for plotting on a chart
        private void ParseResultToChart(string result)
        {
            string[] results = new string[] { };
            List<KeyValuePair<string, double>> chartInput = new List<KeyValuePair<string, double>>();
            txtResult.Visible = false;
            chartControl.Visible = false;

            try
            {
                results = result.Split(new string[] { "\n" }, StringSplitOptions.None);

                if (results?.Length > 0)
                {
                    if (results[0].IndexOf(',') >= 0)
                    {
                        int index = 0;
                        foreach (string res in results)
                        {
                            var axes = res.Split(new string[] { "," }, StringSplitOptions.None);
                            if (index == 0)
                            {
                                if (Double.TryParse(axes[1], out double output))
                                {
                                    chartInput.Add(new KeyValuePair<string, double>(axes[0], Convert.ToDouble(axes[1])));
                                }
                            }
                            else
                            {
                                chartInput.Add(new KeyValuePair<string, double>(axes[0], Convert.ToDouble(axes[1])));
                            }
                            index++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                txtResult.Text = result;
                txtResult.Visible = true;
                MessageBox.Show("I am sorry, I could not parse the results onto a chart, please see the results below displayed as text");
                return;
            }

            if (chartInput.Count > 0)
            {
                this.chartControl.Series.Clear();

                // Data arrays
                string[] seriesArray = chartInput.Select(s => s.Key).ToArray();
                double[] pointsArray = chartInput.Select(s => s.Value).ToArray();

                // Set palette
                this.chartControl.Palette = ChartColorPalette.BrightPastel;

                // Set title
                this.chartControl.Titles.Clear();
                this.chartControl.Titles.Add("Applications");

                // Add series.
                for (int i = 0; i < seriesArray.Length; i++)
                {
                    Series series = this.chartControl.Series.Add(seriesArray[i]);
                    series.Points.Add(pointsArray[i]);
                }
                chartControl.Visible = true;
            }
        }
        private void LoadDataIntoDataGridView(string filePath)
        {
            // Assuming the file contains tabular data, you can use StreamReader to read the data
            using (StreamReader reader = new StreamReader(filePath))
            {
                // Read the data into a DataTable
                DataTable dataTable = new DataTable();
                string[] columns = reader.ReadLine().Split(','); //('\t'); // Adjust the delimiter as needed
                foreach (string column in columns)
                {
                    dataTable.Columns.Add(TitleCase(column.Replace("_", " ")));
                }

                while (!reader.EndOfStream)
                {
                    string[] rowData = reader.ReadLine().Split(','); //('\t'); // Adjust the delimiter as needed
                    dataTable.Rows.Add(rowData);
                }

                // Bind the DataTable to the DataGridView
                dataGridView1.DataSource = dataTable;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    col.Width = dataGridView1.Width / dataGridView1.Columns.Count;
                }
            }
        }
        private string TitleCase(string input)
        {
            string output = string.Empty;
            string[] tokens = input.Split(new string[] { " " }, StringSplitOptions.None);
            string[] tokensUpper = tokens.ToList().Select(s => s[0].ToString().ToUpper() + s.Substring(1, s.Length - 1)).ToArray();
            output = string.Join(" ", tokensUpper);
            return output;
        }
        private void SetupChat()
        {
            string message = string.Empty;
            api.Auth.OpenAIOrganization = organisation;
            chatEndpoint = api.Chat as ChatEndpoint;
            if (System.IO.File.Exists(filePath))
            {
                string input = System.IO.File.ReadAllText(filePath);
                conversation = api.Chat.CreateConversation();
                loadedFile = true;
                conversation.AppendUserInput("This is the input data." + input);

                if (System.IO.File.Exists(reportsList))
                {
                    string reports = System.IO.File.ReadAllText(reportsList);
                    conversation.AppendUserInput("This is my list of reports." + reports);
                }
            }
        }

        private async System.Threading.Tasks.Task<List<Question>> GetQuestions(string question)
        {
            List<Question> questions = new List<Question>();

            if (!loadedFile)
            {
                SetupChat();
            }

            if (question.StartsWith("Bullet point list of reports"))
            {
                //		chat.AppendSystemMessage("List only relevant reports");
                //		chat.AppendUserInput(query);
                //		var response = await chat.GetResponseFromChatbotAsync().ConfigureAwait(false);
                //		
                //		string[] questionsList = response.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                //		if (questionsList?.Length > 0)
                //		{
                //			byte questionIndex = 0;
                //			foreach (var question in questionsList)
                //			{
                //				Question newQuestion = new Question(++questionIndex, question);
                //				questions.Add(newQuestion);
                //			}
                //		}
            }
            else
            {
                question = question + " from input data";
                conversation.AppendUserInput(question);
                string response = string.Empty;

                try
                {
                    var task = Task.Run(() =>
                    {
                        response = conversation.GetResponseFromChatbotAsync().Result;
                    });

                    while (task.Status != TaskStatus.RanToCompletion && task.Status != TaskStatus.Faulted)
                    {
                        Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, task.Status);
                    }

                    if (response == "") ;
                    //response = task.Result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                string[] questionsList = response.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (questionsList?.Length > 0)
                {
                    byte questionIndex = 0;
                    bool isReport = false;
                    byte groupIndex = 1;
                    foreach (var q in questionsList)
                    {
                        string[] parts = q.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 1 && parts[0].Contains("Related Reports"))
                        {
                            isReport = true;
                            questionIndex = 0;
                            groupIndex = 2;
                            continue;
                        }

                        if (parts.Length > 1)
                        {
                            Question newQuestion = new Question(groupIndex, (!isReport ? "Questions" : "Reports"), ++questionIndex, (parts.Length >= 2 ? parts[1] : parts[0]));
                            questions.Add(newQuestion);
                        }
                    }
                }
            }
            return questions;
        }
        private async System.Threading.Tasks.Task<string> GetQuestionResponse(byte questionIndex, string adhocQuestion, string resultType = "")
        {
            string response = string.Empty;

            if (!loadedFile)
            {
                SetupChat();
            }

            string question = string.Empty;

            if (questionIndex > 0)
            {
                question = $"Give me the answer for question {questionIndex}";
            }
            else if (adhocQuestion.Length > 0)
            {
                question = adhocQuestion;
            }
            if (resultType == "Chart")
                question += " in CSV format for plotting on a chart";

            if (question.Length > 0)
            {
                try
                {
                    conversation.AppendUserInput(question);
                    conversation.AppendUserInput("Give me the results only without any explanation or column delimiters");

                    var task = Task.Run(() =>
                    {
                        return conversation.GetResponseFromChatbotAsync();
                    });

                    while (task.Status != TaskStatus.RanToCompletion)
                    {
                        Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, task.Status);
                    }

                    response = task.Result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return response;
        }
        #endregion
    }
}
