using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldYachts.Data.Models
{
    public class Invoice
    {
        /// <summary>
        /// Идентификатор счета
        /// </summary>
        [Required] public int Id { get; set; }
        
        /// <summary>
        /// Идентификатор заказа 
        /// </summary>
        [Required] public int ContractId { get; set; }
        /// <summary>
        /// Оплачен ли?
        /// </summary>
        [Required] public bool Settled { get; set; }
        /// <summary>
        /// Сумма заказа
        /// </summary>
        [Required] public decimal Sum { get; set; }
        /// <summary>
        /// Сумма заказа с НДС
        /// </summary>
        [Required] public decimal SumInclVat { get; set; }
        /// <summary>
        /// Дата оплаты
        /// </summary>
        [Required] public DateTime Date { get; set; }
        
        /// <summary>
        /// Ссылка на счет
        /// </summary>
        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
    }
}
