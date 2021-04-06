﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
{
    public class Partner
    {
        /// <summary>
        /// Идентификатор партнера
        /// </summary>
        public int Id { get; set; }
        
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

        public DateTime CreatedAt { get; set; }
    }
}