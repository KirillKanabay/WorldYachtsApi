using System;
using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
{
    public class OrderModel
    {
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public int SalesPersonId { get; set; }
        
        [Required]
        public DateTime Date { get; set; }
        
        [Required]
        public int BoatId { get; set; }
        
        [Required]
        public string DeliveryAddress { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
