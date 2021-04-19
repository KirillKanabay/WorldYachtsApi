using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Entities
{
    public class Accessory:BaseEntity
    {
        /// <summary>
        /// Название аксессуара
        /// </summary>
        [Required] public string Name { get; set; }
        /// <summary>
        /// Описание аксессуара
        /// </summary>
        [Required] public string Description { get; set; }
        /// <summary>
        /// Стоимость аксессуара
        /// </summary>
        [Required] public decimal Price { get; set; }
        /// <summary>
        /// НДС
        /// </summary>
        [Required] public double Vat { get; set; }
        /// <summary>
        /// Количество на складе
        /// </summary>
        [Required] public int Inventory { get; set; }
        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        [Required] public int PartnerId { get; set; }

        [ForeignKey("PartnerId")]
        public virtual Partner Partner { get; set; }

        /// <summary>
        /// Ссылка на доступные аксессуары для определенных лодок
        /// </summary>
        [ForeignKey("AccessoryId")]
        public virtual List<AccessoryToBoat> AccessoryToBoat { get; set; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
