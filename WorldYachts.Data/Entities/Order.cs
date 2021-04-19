using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Entities
{
    public class Order:BaseEntity
    {
        /// <summary>
        /// Идентификатор заказчика
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Идентификатор менеджера
        /// </summary>
        [Required]
        public int SalesPersonId { get; set; }

        /// <summary>
        /// Дата формирования доставки
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор лодки
        /// </summary>
        [Required]
        public int BoatId { get; set; }

        /// <summary>
        /// Адрес доставки
        /// </summary>
        [Required]
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// Город доставки
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Required]
        public int Status { get; set; }

        #region Связи

        /// <summary>
        /// Ссылка на менеджера
        /// </summary>
        public virtual SalesPerson SalesPerson { get; set; }

        /// <summary>
        /// Ссылка на заказчика
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Ссылка на лодку
        /// </summary>
        public virtual Boat Boat { get; set; }

        /// <summary>
        /// Ссылка на список деталей заказа
        /// </summary>
        public virtual List<OrderDetail> OrderDetails { get; set; }

        #endregion


    }
}
