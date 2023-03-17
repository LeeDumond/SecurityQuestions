using Newtonsoft.Json;

namespace SecurityQuestions.Models
{
    public class User
    {
        public string Name { get; set; }

        [JsonProperty("question")]
        public List<Question> Questions { get; set; }
    }
}