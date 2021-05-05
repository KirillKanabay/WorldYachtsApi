using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorldYachts.Data.Entities
{
    public class Contract:BaseEntity
    {
        /// <summary>
        /// Идентификатор доставки
        /// </summary>
        [Required] public int OrderId { get; set; }

        /// <summary>
        /// Дата оформления контракта
        /// </summary>
        [Required] public DateTime Date { get; set; }

        /// <summary>
        /// Погашенная часть денег
        /// </summary>
        [Required] public decimal DepositPayed { get; set; }

        /// <summary>
        /// Общая стоимость заказа
        /// </summary>
        [Required] public decimal ContractTotalPrice { get; set; }

        /// <summary>
        /// Общая стоимость заказа включая НДС
        /// </summary>
        [Required] public decimal ContractTotalPriceInclVat { get; set; }

        /// <summary>
        /// Процесс выполнения заказа
        /// </summary>
        [Required] public string ProductionProcess { get; set; }

        /// <summary>
        /// Ссылка на доставку
        /// </summary>
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
