﻿using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
{
    public class OrderDetails
    {
        /// <summary>
        /// Идентификатор критериев доставки
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
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
        public Accessory Accessory { get; set; }
        // /// <summary>
        // /// Ссылка на заказ
        // /// </summary>
        public virtual Order Order { get; set; }
    }
}
