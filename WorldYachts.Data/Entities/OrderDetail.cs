using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Entities
{
    public class OrderDetail:BaseEntity
    {
        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        [Required] public int AccessoryId { get; set; }
        
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        [Required] public int OrderId { get; set; }

        /// <summary>
        /// Ссылка на аксессуар
        /// </summary>
        [ForeignKey("AccessoryId")]
        public virtual Accessory Accessory { get; set; }

        /// <summary>
        /// Ссылка на заказ
        /// </summary>
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
