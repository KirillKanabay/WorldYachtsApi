using System;
using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Entities
{
    public class Customer : BaseEntity
    {
        /// <summary>
        /// Имя клиента
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        [Required]
        public string SecondName { get; set; }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        [Required]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Адрес клиента
        /// </summary>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Город клиента
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        [Required]
        public string Phone { get; set; }

        /// <summary>
        /// Email клиента
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Название организации клиента
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Серия документа
        /// </summary>
        [Required]
        public string IdNumber { get; set; }

        /// <summary>
        /// Название документа
        /// </summary>
        [Required]
        public string IdDocumentName { get; set; }
    }
}
