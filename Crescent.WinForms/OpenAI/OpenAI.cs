using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crescent.WinForms.Model;
using OpenAI_API;
using OpenAI_API.Chat;

namespace Crescent.WinForms.OpenAI
{
    internal class OpenAI
    {
        private OpenAIAPI _openAIAPI;
        private Conversation _conversation;
        private const string apiKey = "";
        private const string orgKey = "";
        private const string _sampleReports = "Application Summary Report (RC);Applications by Status Report (RC);Sub-divisions by Year (RC);Customer Summary (BI)";

        private OpenAIAPI OpenAIAPI 
        { 
            get => _openAIAPI; 
            set => _openAIAPI = value; 
        }

        public OpenAI()
        {
            _openAIAPI = new OpenAIAPI(new APIAuthentication(apiKey, orgKey));
        }

        public void SetupChat(string dataSet)
        {
            Debug.WriteLine("Start OpenAI Setup...");
            if(_conversation == null)
            {
                _conversation = _openAIAPI.Chat.CreateConversation();
            }

            _conversation.AppendUserInput("This is the input data. " + dataSet);
            _conversation.AppendUserInput("These are sample reports that I currently have. " + _sampleReports);

            Debug.WriteLine("End OpenAI Setup...");
        }

        public async System.Threading.Tasks.Task<List<Question>> GetQuestions(string query)
        {
            Debug.WriteLine("Start Generation QUestions...");
            List<Question> questions = new List<Question>();

            if (query == "Bullet point list of reports that match the data")
            {
                _conversation.AppendSystemMessage("List only relevant reports");
                _conversation.AppendUserInput(query);
                var response = await _conversation.GetResponseFromChatbotAsync().ConfigureAwait(true);

                string[] questionsList = response.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (questionsList?.Length > 0)
                {
                    byte questionIndex = 0;
                    foreach (var question in questionsList)
                    {
                        Question newQuestion = new Question(++questionIndex, question);
                        questions.Add(newQuestion);
                        Debug.WriteLine($"Question generated : {question}");
                    }
                }
            }
            else
            {
                _conversation.AppendUserInput("Look for questions only from the data");
                _conversation.AppendUserInput(query);
                var response = await _conversation.GetResponseFromChatbotAsync().ConfigureAwait(true);

                string[] questionsList = response.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (questionsList?.Length > 0)
                {
                    byte questionIndex = 0;
                    foreach (var question in questionsList)
                    {
                        string[] parts = question.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        Question newQuestion = new Question(++questionIndex, parts[1]);
                        questions.Add(newQuestion);
                        Debug.WriteLine($"Question generated : {newQuestion.QuestionName}");

                    }
                }
            }

            Debug.WriteLine($"Question generation completed.");

            return questions;
        }

        public async System.Threading.Tasks.Task<string> GetQuestionResponse(byte questionIndex, string adhocQuestion, string resultType = "")
        {
            string response = string.Empty;

            if (_conversation == null)
                return string.Empty;

            string question = string.Empty;

            if (questionIndex > 0)
            {
                question = $"Answer question {questionIndex}";
                if (resultType.Length > 0)
                    question += " for " + resultType + " comma separated table";
                _conversation.AppendUserInput(question);
                response = await _conversation.GetResponseFromChatbotAsync().ConfigureAwait(true);
                //response.Dump($"Answer question {questionIndex}");
            }
            else if (adhocQuestion.Length > 0)
            {
                question = adhocQuestion;
                if (resultType.Length > 0)
                    question += " for " + resultType + " comma separated table";
                _conversation.AppendUserInput(question);
                response = await _conversation.GetResponseFromChatbotAsync().ConfigureAwait(true);
                //response.Dump(adhocQuestion);
            }
            return response;
        }

    }
}
