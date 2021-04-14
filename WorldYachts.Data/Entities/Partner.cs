using System;
using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class Partner : BaseEntity
    {
        /// <summary>
        /// Название партнера
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        [Required]
        [StringLength(64)]
        public string Address { get; set; }

        /// <summary>
        /// Город нахождения партнера
        /// </summary>
        [Required]
        [StringLength(64)]
        public string City { get; set; }
    }
}