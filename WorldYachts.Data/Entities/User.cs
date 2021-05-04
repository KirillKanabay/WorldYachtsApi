using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
