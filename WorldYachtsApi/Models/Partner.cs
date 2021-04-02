using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
{
    public class Partner
    {
        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        /// <summary>
        /// Название партнера
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Адрес партнера
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Город нахождения партнера
        /// </summary>
        [Required]
        public string City { get; set; }
    }
}