using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Models
{
    public class SalesPerson
    {
        /// <summary>
        /// Идентификатор менеджера 
        /// </summary>
        [Required] public int Id { get; set; }
        
        /// <summary>
        /// Имя менеджера
        /// </summary>
        [Required] public string Name { get; set; }
        
        /// <summary>
        /// Фамилия менеджера
        /// </summary>
        [Required] public string SecondName { get; set; }

        [ForeignKey("SalesPersonId")]
        public List<Order> Orders { get; set; }
    }
}
