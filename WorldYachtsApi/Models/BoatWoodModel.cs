using System.ComponentModel.DataAnnotations;


namespace WorldYachtsApi.Models
{
    public class BoatWoodModel
    {
        [MaxLength(64)]
        public string Wood { get; set; }
    }
}
