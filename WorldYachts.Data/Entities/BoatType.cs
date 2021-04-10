using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class BoatType:BaseEntity
    {
        [Required] public string Type { get; set; }
    }
}
