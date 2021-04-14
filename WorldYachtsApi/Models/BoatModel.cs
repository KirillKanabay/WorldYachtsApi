using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
{
    public class BoatModel
    {
        [MaxLength(128)]
        public string Model { get; set; }
        public int TypeId { get; set; }
        [Range(0,32)]
        public int NumberOfRowers { get; set; }
        public bool Mast { get; set; }
        public string Color { get; set; }
        public int WoodId { get; set; }
        [Range(0, double.MaxValue)]
        public decimal BasePrice { get; set; }
        [Range(0, 100)]
        public double Vat { get; set; }
    }
}
