using Newtonsoft.Json;
using SecurityQuestions.Models;

namespace SecurityQuestions.Data
{
    public class UserData
    {
        [JsonProperty("user")]
        public List<User> Users { get; set; }
    }
}