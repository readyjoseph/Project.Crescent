using Microsoft.VisualBasic;
using OpenAI_API;
using OpenAI_API.Chat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Crescent.WinForms.DTO;
using OpenAI_API.Models;
using OpenAI_API.Completions;
using System.Collections;
using System.Configuration;
using System.Reflection.Metadata.Ecma335;

namespace Crescent.WinForms
{
    public partial class DataLoader_v1 : Form
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

        //private static string jsonFilePath => System.IO.Path.GetDirectoryName(filePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(filePath) + ".jsonl";
        //private static string jsonFileName => System.IO.Path.GetFileName(jsonFilePath);
        private static string reportsList;
        private static bool loadedFile = false;
        private static bool loadedReports = false;
        #endregion

        #region Constructor
        public DataLoader_v1()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        private void DataLoader_Load(object sender, EventArgs e)
        {
            cmbAnalysisType.Items.Clear();
            cmbAnalysisType.Items.Add("Fixed Questions");
            cmbAnalysisType.Items.Add("Free Text");

            cmbAnalysisType.SelectedIndex = 0;
            cmbAnalysisType_SelectedIndexChanged(cmbAnalysisType, new EventArgs());

            cmbFixedQuestions.Items.Clear();
            cmbFixedQuestions.Items.Add("Trend questions");
            cmbFixedQuestions.Items.Add("Grouped queries");
            cmbFixedQuestions.Items.Add("Summary questions");
            cmbFixedQuestions.SelectedIndex = 0;

            cmbShowResultsAs.Items.Clear();
            cmbShowResultsAs.Items.Add("Text");
            cmbShowResultsAs.Items.Add("Chart");
            cmbShowResultsAs.SelectedIndex = 0;

            apiKey = ConfigurationManager.AppSettings["OpenAI.APIKey"].ToString();
            organisation = ConfigurationManager.AppSettings["OpenAI.OrganisationId"].ToString();
            reportsList = ConfigurationManager.AppSettings["TestData.ReportsPath"].ToString();

            api = new OpenAIAPI(apiKey);
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
                Filter = "txt files (*.txt)|*.txt|CSV files (*.csv)|*.CSV",
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
            }
        }
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            CompletionResult completions = null;
            List<Question> questions = null;
            string answer = string.Empty;
            //string question = string.Empty;
            if (!loadedFile)
                SetupChat();

            if (cmbAnalysisType.Items[cmbAnalysisType.SelectedIndex] as string == "Fixed Questions")
            {
                questions = GetQuestions("List of " + cmbFixedQuestions.Items[cmbFixedQuestions.SelectedIndex]).Result;
                panel1.Controls.Clear();
                int top = 0;
                foreach (var question in questions)
                {
                    LinkLabel btn = new LinkLabel();
                    btn.Text = $"{question.QuestionIndex} {question.QuestionName}";
                    btn.Tag = question.QuestionIndex;
                    btn.Left = 20;
                    btn.Top = top;
                    btn.Width = panel1.Width - 50;
                    top += 20;
                    btn.Click += (sender, e) =>
                    {
                        answer = GetQuestionResponse(Convert.ToByte((sender as LinkLabel).Tag), string.Empty).Result;
                        txtResult.Text = "";
                        txtResult.Text = answer;
                    };
                    panel1.Controls.Add(btn);
                }
                panel1.AutoScroll = true;
            }
            else
            {
                answer = GetQuestionResponse(0, txtFreeText.Text).Result;

                if (cmbShowResultsAs.Items[cmbShowResultsAs.SelectedIndex] as string == "Text")
                {
                    txtResult.Text = "";
                    txtResult.Text = answer;
                }
                else if (cmbShowResultsAs.Items[cmbShowResultsAs.SelectedIndex] as string == "Chart")
                {
                    ParseResultToChart(answer);
                }
            }
        }
        //private void btnFreeText_Click(object sender, EventArgs e)
        //{
        //    if (!loadedFile)
        //        SetupChat();

        //    string answer = GetQuestionResponse(0, txtFreeText.Text).Result;
        //    txtResult.Text = "";
        //    txtResult.Text = answer;
        //}
        #endregion

        #region Private Methods
        private void ParseResultToChart(string result, string resultType = "CSV")
        {
            string[] results = new string[] { };
            List<KeyValuePair<string, int>> chartInput = new List<KeyValuePair<string, int>>();

            if (resultType == "CSV")
            {
                results = result.Split(new string[] { "," }, StringSplitOptions.None);
            }

            if (results?.Length > 0)
            {
                if (results[0].IndexOf(':') >= 0)
                {
                    foreach (string res in results)
                    {
                        var axes = res.Split(new string[] { ":" }, StringSplitOptions.None);
                        chartInput.Add(new KeyValuePair<string, int>(axes[0], Convert.ToInt32(axes[1])));
                    }
                }
            }

            if (chartInput.Count > 0)
            {

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
                    dataTable.Columns.Add(column.Replace("_", " "));
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
                    foreach (var q in questionsList)
                    {
                        string[] parts = q.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length > 0)
                        {
                            Question newQuestion = new Question(++questionIndex, (parts.Length >= 2 ? parts[1] : parts[0]));
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
            if (resultType.Length > 0)
                question += " for " + resultType + " comma separated table";

            if (question.Length > 0)
            {
                try
                {
                    conversation.AppendUserInput(question);

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

        private void cmbAnalysisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFreeText.Visible = cmbAnalysisType.Items[cmbAnalysisType.SelectedIndex] as string == "Free Text";
            cmbFixedQuestions.Visible = cmbAnalysisType.Items[cmbAnalysisType.SelectedIndex] as string == "Fixed Questions";
        }
    }
}
