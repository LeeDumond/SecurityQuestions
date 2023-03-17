using SecurityQuestions.Data;
using SecurityQuestions.Models;

namespace SecurityQuestions.Services
{
    public class SecurityQuestionService : ISecurityQuestionService
    {
        private readonly IDataRepository _repository;

        public SecurityQuestionService(IDataRepository repository)
        {
            _repository = repository;
        }

        public User GetUserByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return _repository.GetUserByName(name);
        }

        public List<Question> GetQuestionsForUserInRandomOrder(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            Random rnd = new Random();

            return user.Questions.OrderBy(q => rnd.Next()).ToList();
        }

        public List<string> GetQuestions()
        {
            return _repository.GetQuestions();
        }

        public bool CheckAnswer(Question question, string providedAnswer)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            return providedAnswer != null &&
                   providedAnswer.Equals(question.Answer, StringComparison.InvariantCultureIgnoreCase);
        }

        public void SaveUserData()
        {
            _repository.SaveUserData();
        }
    }
}