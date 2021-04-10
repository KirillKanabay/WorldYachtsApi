using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class BoatWood : BaseEntity
    {
        [Required] public string Wood { get; set; }
    }
}
