using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Entities
{
    public class Boat:BaseEntity
    {
        /// <summary>
        /// Модель яхты
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Тип лодки
        /// </summary>
        [Required]
        public int TypeId { get; set; }

        /// <summary>
        /// Количество мест для гребцов
        /// </summary>
        [Required]
        public int NumberOfRowers { get; set; }

        /// <summary>
        /// Наличие мачты
        /// </summary>
        [Required]
        public bool Mast { get; set; }

        /// <summary>
        /// Цвет яхты
        /// </summary>
        [Required]
        public string Color { get; set; }

        /// <summary>
        /// Тип дерева
        /// </summary>
        [Required]
        public int WoodId { get; set; }

        /// <summary>
        /// Цена без НДС
        /// </summary>
        [Required]
        public decimal BasePrice { get; set; }

        /// <summary>
        /// НДС%
        /// </summary>
        [Required]
        public double Vat { get; set; }

        #region Связи

        [ForeignKey("BoatId")]
        public virtual IEnumerable<AccessoryToBoat> AccessoryToBoat { get; set; }

        [ForeignKey("TypeId")]
        public virtual BoatType BoatType { get; set; }

        [ForeignKey("WoodId")]
        public virtual BoatWood BoatWood { get; set; }

        public virtual IEnumerable<Order> Orders { get; set; }
        #endregion
    }
}
