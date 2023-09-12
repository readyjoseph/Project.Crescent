<Query Kind="Program">
  <NuGetReference>OpenAI</NuGetReference>
  <Namespace>OpenAI_API</Namespace>
  <Namespace>OpenAI_API.Chat</Namespace>
  <Namespace>OpenAI_API.Completions</Namespace>
  <Namespace>OpenAI_API.Embedding</Namespace>
  <Namespace>OpenAI_API.Files</Namespace>
  <Namespace>OpenAI_API.Images</Namespace>
  <Namespace>OpenAI_API.Models</Namespace>
  <Namespace>OpenAI_API.Moderation</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

string apiKey = "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";
string organisation = "org-BtV4Ff7q0LPWT1KgS2Q1BWb8";
public OpenAIAPI api => new OpenAIAPI(apiKey);
public CompletionResult chat;
string fileName => @"C:\OpenAI\TestData\TP_Applications_For_AskCSV_2.txt";
string reportsList => @"C:\OpenAI\TestData\Reports.txt";
bool loadedFile = false;
bool loadedReports = false;

void Main()
{
	List<Question> questions = null;
	string answer = string.Empty;
	string question = string.Empty;
	
	api.Auth.OpenAIOrganization = organisation;
	SetupCompletion(fileName, reportsList);
	
	//questions = GetQuestions("List trend questions").Result;
	//questions.Dump("List trend questions");
	//if (questions.Count() > 0)
	//{
	//	int questionIndex = new Random(1).Next(questions.Count());
	//	answer = GetQuestionResponse((byte)questionIndex, string.Empty).Result;
	//	answer.Dump($"Answer for question {questionIndex}");
	//}
	
	//answer = GetQuestionResponse(0, "What is the trend of closed applications").Result;
	//answer.Dump($"What is the trend of closed applications");

	//questions = GetQuestions("List of grouped queries").Result;
	//questions.Dump("List of grouped queries");
	
	//questions = GetQuestions("List of summary questions").Result;
	//questions.Dump("List of summary questions");
	//if (questions.Count() > 0)
	//{
	//	int questionIndex = new Random(1).Next(questions.Count());
	//	answer = GetQuestionResponse((byte)questionIndex, string.Empty, "chart").Result;
	//	answer.Dump($"Answer for question {questionIndex}");
	//}

	//questions = GetQuestions("List of grouping questions").Result;
	//questions.Dump("Number of grouping questions");
	//if (questions.Count() > 0)
	//{
	//	int questionIndex = new Random(1).Next(questions.Count());
	//	answer = GetQuestionResponse((byte)questionIndex, string.Empty, "chart").Result;
	//	answer.Dump($"Answer for question {questionIndex}");
	//}

	//question = "Number of applications grouped by status";
	//answer = GetQuestionResponse(0, question, "chart").Result;
	//answer.Dump($"Answer for question \"{question}\"");

	//answer = GetQuestionResponse(0, "What is the count of status_name equals Withdrawn").Result;
	//answer.Dump($"What is the count of status_name equals Withdrawn");
	//questions = GetQuestions("Bullet point list of reports that match the data").Result;
	//questions.Dump("Bullet point list of reports that match the data");
}

async void SetupCompletion()
{
	if (chat == null)
	{
		CompletionRequest req = new CompletionRequest() {
			Model = "gpt-3.5-turbo",
			Prompt= "You will be provided with unstructured data, and your task is to parse it into CSV format.",
  			Temperature= 0,
  			MaxTokens= 256
		};

		chat = await api.Completions.CreateCompletionAsync(req).ConfigureAwait(true);
		var response = await chat.Completions().ConfigureAwait(true);
	}
//	chat.RequestParameters.Temperature = 0.5;
//
//	if (!loadedFile && System.IO.File.Exists(fileName))
//	{
//		string input = System.IO.File.ReadAllText(fileName);
//		chat.AppendUserInput("This is the input data." + input);
//		loadedFile = true;
//	}
//	
//	if (!loadedReports && System.IO.File.Exists(reportsList))
//	{
//		string reports = System.IO.File.ReadAllText(reportsList);
//		chat.AppendUserInput(reports);
//	}
}

// Define other methods and classes here
//async System.Threading.Tasks.Task<List<Question>> GetQuestions(string query)
//{
//	List<Question> questions = new List<Question>();
//
//	if (query == "Bullet point list of reports that match the data")
//	{
//		chat.AppendSystemMessage("List only relevant reports");
//		chat.AppendUserInput(query);
//		var response = await chat.GetResponseFromChatbotAsync().ConfigureAwait(true);
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
//	}
//	else
//	{
//		chat.AppendUserInput("Look for questions only from the data");
//		chat.AppendUserInput(query);
//		var response = await chat.GetResponseFromChatbotAsync().ConfigureAwait(true);
//		
//		string[] questionsList = response.Split(new string[] { "\n"}, StringSplitOptions.RemoveEmptyEntries);
//		if (questionsList?.Length > 0)
//		{
//			byte questionIndex = 0;
//			foreach (var question in questionsList)
//			{
//				string[] parts = question.Split(new string[] { "."}, StringSplitOptions.RemoveEmptyEntries);
//				Question newQuestion = new Question(++questionIndex, parts[1]);				
//				questions.Add(newQuestion);
//			}
//		}
//	}
//	return questions;
//}
//async System.Threading.Tasks.Task<string> GetQuestionResponse(byte questionIndex, string adhocQuestion, string resultType = "")
//{
//	string response = string.Empty;
//	
//	if (chat == null)
//		return string.Empty;
//
//	if (!loadedFile)
//		return string.Empty;
//
//	string question = string.Empty;
//	
//	if (questionIndex > 0)
//	{
//		question = $"Answer question {questionIndex}";
//		if (resultType.Length > 0)
//			question += " for " + resultType + " comma separated table";
//		chat.AppendUserInput(question);
//		response = await chat.GetResponseFromChatbotAsync().ConfigureAwait(true);
//		//response.Dump($"Answer question {questionIndex}");
//	}
//	else if (adhocQuestion.Length > 0)
//	{
//		question = adhocQuestion;
//		if (resultType.Length > 0)
//			question += " for " + resultType + " comma separated table";
//		chat.AppendUserInput(question);
//		response = await chat.GetResponseFromChatbotAsync().ConfigureAwait(true);
//		//response.Dump(adhocQuestion);
//	}
//	return response;
//}

public class Question
{
	public Question(byte questionIndex, string questionName)
	{
		QuestionIndex = questionIndex;
		QuestionName = questionName;
	}
	public Question(byte groupIndex, string groupName, byte questionIndex, string questionName)
	{
		GroupIndex = groupIndex;
		GroupName = groupName;

		QuestionIndex = questionIndex;
		QuestionName = questionName;
	}

	public byte GroupIndex { get; set; }
	public string GroupName { get; set; }
	public byte QuestionIndex { get; set; }
	public string QuestionName { get; set; }
}