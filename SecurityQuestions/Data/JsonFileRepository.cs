using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SecurityQuestions.Models;
using System.Reflection;

namespace SecurityQuestions.Data
{
    public class JsonFileRepository : IDataRepository
    {
        private const string USER_DATA_FILE_NAME = "data.json";
        private const string QUESTIONS_FILE_NAME = "questions.json";
        private static UserData _userData;

        public User GetUserByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (_userData != null)
            {
                return _userData.Users.Find(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }

            string content = File.ReadAllText(GetPath(USER_DATA_FILE_NAME));

            JsonSerializerSettings settings =
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            _userData = JsonConvert.DeserializeObject<UserData>(content, settings);

            if (_userData == null)
            {
                throw new InvalidOperationException("No valid user data was found in the system.");
            }

            return _userData.Users.Find(u => u.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public List<string> GetQuestions()
        {
            string content = File.ReadAllText(GetPath(QUESTIONS_FILE_NAME));

            QuestionData questionData = JsonConvert.DeserializeObject<QuestionData>(content);

            if (questionData == null)
            {
                throw new InvalidOperationException("No valid question data was found in the system.");
            }

            return questionData.Questions;
        }

        public void SaveUserData()
        {
            File.WriteAllText(GetPath(USER_DATA_FILE_NAME), JsonConvert.SerializeObject(_userData));
        }

        private string GetPath(string fileName)
        {
            return Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                throw new InvalidOperationException(), $@"Data\{fileName}");
        }
    }
}