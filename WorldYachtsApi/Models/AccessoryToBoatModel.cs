using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WorldYachtsApi.Models
{
    public class AccessoryToBoatModel
    {
        [Range(0, Double.MaxValue)]
        public int AccessoryId { get; set; }
        
        [Range(0, Double.MaxValue)]
        public int BoatId { get; set; }
    }
}
