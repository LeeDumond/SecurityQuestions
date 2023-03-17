using Newtonsoft.Json;

namespace SecurityQuestions.Data
{
    public class QuestionData
    {
        [JsonProperty("question")]
        public List<string> Questions { get; set; }
    }
}