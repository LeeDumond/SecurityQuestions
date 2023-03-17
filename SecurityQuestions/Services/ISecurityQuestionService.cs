using SecurityQuestions.Models;

namespace SecurityQuestions.Services;

public interface ISecurityQuestionService
{
    User GetUserByName(string name);
    List<Question> GetQuestionsForUserInRandomOrder(User user);
    List<string> GetQuestions();
    bool CheckAnswer(Question question, string providedAnswer);
    void SaveUserData();
}