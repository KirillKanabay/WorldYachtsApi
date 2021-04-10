using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class Admin:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SecondName { get; set; }
    }
}
