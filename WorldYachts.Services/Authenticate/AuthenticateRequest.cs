using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Services.Models.Authenticate
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; } //TODO: Хранить хеш
    }
}
