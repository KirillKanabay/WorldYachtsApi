using System;
using System.Collections.Generic;
using System.Text;

namespace WorldYachts.Services.Models
{
    public class BoatModel
    {
        public string Model { get; set; }
        public int TypeId { get; set; }
        public int NumberOfRowers { get; set; }
        public bool Mast { get; set; }
        public string Color { get; set; }
        public int WoodId { get; set; }
        public decimal BasePrice { get; set; }
        public double Vat { get; set; }
    }
}
