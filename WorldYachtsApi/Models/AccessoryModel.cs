using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldYachtsApi.Models
{
    public class AccessoryModel
    {
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public decimal Price { get; set; } 
        [Required] public double Vat { get; set; }
        [Required] public int Inventory { get; set; }
        [Required] public int PartnerId { get; set; }
    }
}
