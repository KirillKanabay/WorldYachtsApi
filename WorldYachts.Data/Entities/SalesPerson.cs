using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class SalesPerson : BaseEntity
    {
        /// <summary>
        /// Имя менеджера
        /// </summary>
        [Required] public string FirstName { get; set; }

        /// <summary>
        /// Фамилия менеджера
        /// </summary>
        [Required] public string SecondName { get; set; }
        
        public virtual IEnumerable<Order> Orders { get; set; }
    }
}
