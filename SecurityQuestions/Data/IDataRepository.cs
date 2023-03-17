using SecurityQuestions.Models;

namespace SecurityQuestions.Data;

public interface IDataRepository
{
    User GetUserByName(string name);
    List<string> GetQuestions();
    void SaveUserData();
}