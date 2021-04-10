using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class Admin:BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
    }
}
