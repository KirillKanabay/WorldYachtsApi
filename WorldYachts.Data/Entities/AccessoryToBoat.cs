using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Entities
{
    public class AccessoryToBoat:BaseEntity
    {
        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        [Required] public int BoatId { get; set; }
        /// <summary>
        /// Идентификатор аксессуара
        /// </summary>
        [Required] public int AccessoryId { get; set; }

        /// <summary>
        /// Ссылка на лодку
        /// </summary>
        public virtual Boat Boat { get; set; }
        /// <summary>
        /// Ссылка на аксессуар
        /// </summary>
        public virtual Accessory Accessory { get; set; }
    }
}
