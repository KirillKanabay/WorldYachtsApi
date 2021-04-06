using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Models
{
    public class Boat
    {
        /// <summary>
        /// Идентификатор яхты
        /// </summary>
        [Required]
        public int Id { get; set; }
        
        /// <summary>
        /// Модель яхты
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Тип лодки
        /// </summary>
        [Required]
        public string Type { get; set; }

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
        public string Wood { get; set; }

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

        /// <summary>
        /// Ссылка на доступные аксессуары для определенных лодок
        /// </summary>
        [ForeignKey("BoatId")]
        public List<AccessoryToBoat> AccessoryToBoat { get; set; }
    }
}
