using System.Text.Json.Serialization;
using WorldYachtsApi.Entity;

namespace WorldYachtsApi.Entities
{
    public class User : BaseEntity
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public long UserId { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
