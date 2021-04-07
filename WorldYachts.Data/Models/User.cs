using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Models
{
    public class User
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
