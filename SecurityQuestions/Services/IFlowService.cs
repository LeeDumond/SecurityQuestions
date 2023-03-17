using SecurityQuestions.Models;

namespace SecurityQuestions.Services;

public interface IFlowService
{
    void RunInitialFlow();

    // implementations are made public for unit testing purposes
    void RunAnswerFlow(User user);
    void RunStoreFlow(User user);
}