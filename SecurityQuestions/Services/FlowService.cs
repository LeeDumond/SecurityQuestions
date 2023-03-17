using SecurityQuestions.Models;

namespace SecurityQuestions.Services
{
    public class FlowService : IFlowService
    {
        private readonly ISecurityQuestionService _service;

        public FlowService(ISecurityQuestionService securityQuestionService)
        {
            _service = securityQuestionService;
        }

        public void RunInitialFlow()
        {
            Console.WriteLine("\nHi, what is your name?");

            string name = Console.ReadLine();

            User user = _service.GetUserByName(name);

            if (user == null)
            {
                Console.WriteLine("User Not Found.");
            }
            else
            {
                Console.Write($"\nHello {user.Name}. ");

                if (user.Questions.Any())
                {
                    if (IsAffirmativeResponse("Do you want to answer a security question?"))
                    {
                        RunAnswerFlow(user);
                    }
                    else
                    {
                        RunStoreFlow(user);
                    }
                }
                else
                {
                    RunStoreFlow(user);
                }
            }
        }

        public void RunAnswerFlow(User user)
        {
            bool success = false;
            int questionsAsked = 0;
            List<Question> userQuestions = _service.GetQuestionsForUserInRandomOrder(user);

            foreach (Question question in userQuestions)
            {
                Console.WriteLine($"\n{question.Text}");

                success = _service.CheckAnswer(question, Console.ReadLine());

                questionsAsked++;

                if (success)
                {
                    Console.WriteLine("\nCongratulations, that is correct! :)");
                    break;
                }

                Console.WriteLine("\nSorry, that is incorrect.");

                if (questionsAsked < userQuestions.Count)
                {
                    Console.WriteLine("Let's try a different question.");
                }
            }

            if (!success)
            {
                Console.WriteLine("There are no more questions to ask. :(");
            }

            RunInitialFlow();
        }

        public void RunStoreFlow(User user)
        {
            if (IsAffirmativeResponse("\nWould you like to store answers to security questions?"))
            {
                user.Questions.Clear();

                Console.WriteLine("\nType your answer, or press Enter to skip the question.");

                List<string> newQuestions = _service.GetQuestions();

                foreach (string question in newQuestions)
                {
                    Console.WriteLine($"\n{question}");

                    string answer = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(answer))
                    {
                        continue;
                    }

                    user.Questions.Add(new Question { Text = question, Answer = answer });

                    if (user.Questions.Count == 3)
                    {
                        _service.SaveUserData();
                        Console.WriteLine("\nYou have stored answers for 3 questions.");
                        break;
                    }
                }

                if (user.Questions.Count < 3)
                {
                    if (IsAffirmativeResponse(
                            "\nYou must provide answers to three questions. Would you like to try again?"))
                    {
                        RunStoreFlow(user);
                    }
                }
            }

            RunInitialFlow();
        }

        private static bool IsAffirmativeResponse(string inquiry)
        {
            Console.Write($"{inquiry} (Y/N) ");

            ConsoleKeyInfo entry = Console.ReadKey();

            Console.WriteLine();

            return entry.Key == ConsoleKey.Y;
        }
    }
}