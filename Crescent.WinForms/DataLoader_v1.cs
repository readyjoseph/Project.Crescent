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

namespace Crescent.WinForms
{
    public partial class DataLoader_v1 : Form
    {
        #region Variables
        public const string apiKey = "";
        public const string organisation = "";
        public static OpenAIAPI api => new OpenAIAPI(apiKey);
        public static ChatEndpoint chatEndpoint;
        public static Conversation conversation;
        //public ChatResult chat;
        public const string filePath = @"C:\OpenAI\TestData\fine-tuning-data.txt";
        public static string fileName = System.IO.Path.GetFileName(filePath);
        public static string jsonFilePath => System.IO.Path.GetDirectoryName(filePath) + "\\" + System.IO.Path.GetFileNameWithoutExtension(filePath) + ".jsonl";
        public static string jsonFileName => System.IO.Path.GetFileName(jsonFilePath);
        public static string reportsList => @"C:\OpenAI\TestData\Reports.txt";
        public static bool loadedFile = false;
        public static bool loadedReports = false;
        #endregion

        public DataLoader_v1()
        {
            InitializeComponent();
        }

        #region Event Handlers
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
            }
        }
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            CompletionResult completions = null;
            List<Question> questions = null;
            string answer = string.Empty;
            //string question = string.Empty;
            loadedFile = false;
            SetupChat();

            //var task = Task.Run(() =>
            //{
            //    return api.Completions.CreateCompletionAsync(
            //        prompt: "What is the meaning of life?",
            //        model: "text-davinci-002",
            //        //max_tokens: 20,
            //        temperature: 0.5f
            //    );
            //});

            //while (task.Status != TaskStatus.RanToCompletion)
            //{
            //    Console.WriteLine("Thread ID: {0}, Status: {1}", Thread.CurrentThread.ManagedThreadId, task.Status);
            //}

            //completions = task.Result;
            //foreach (var completion in completions.Completions)
            //{
            //    MessageBox.Show(completion.Text);
            //}


            questions = GetQuestions("List of " + cmbAnalysisType.Items[cmbAnalysisType.SelectedIndex]).Result;
            panel1.Controls.Clear();
            int top = 0;
            foreach (var question in questions)
            {
                LinkLabel btn = new LinkLabel();
                btn.Text = $"{question.QuestionIndex} {question.QuestionName}";
                btn.Tag = question.QuestionIndex;
                btn.Top = top;
                btn.Width = panel1.Width - 50;
                top += 20;
                btn.Click += (sender, e) =>
                {
                    answer = GetQuestionResponse(Convert.ToByte((sender as LinkLabel).Tag), string.Empty).Result;
                    listBox1.Items.Clear();
                    listBox1.Items.Add(answer);
                };
                panel1.Controls.Add(btn);
                //listBox1.Items.Add(new ListViewItem(question.QuestionName));
            }
            panel1.AutoScroll = true;

            ////questions.Dump("List of summary questions");
            //if (questions.Count() > 0)
            //{
            //    int questionIndex = new Random(1).Next(questions.Count());
            //    answer = GetQuestionResponse((byte)questionIndex, string.Empty, "chart").Result;
            //    listBox1.Items.Add(new ListViewItem(answer));
            //}
        }
        #endregion

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

        private void DataLoader_Load(object sender, EventArgs e)
        {
            cmbAnalysisType.Items.Clear();
            cmbAnalysisType.Items.Add("Trend questions");
            cmbAnalysisType.Items.Add("Grouped queries");
            cmbAnalysisType.Items.Add("Summary questions");
            cmbAnalysisType.SelectedIndex = 0;
        }
    }
}
