using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Models
{
    public class SalesPerson
    {
        /// <summary>
        /// Идентификатор менеджера 
        /// </summary>
        [Required] public int Id { get; set; }

        /// <summary>
        /// Имя менеджера
        /// </summary>
        [Required] public string Name { get; set; }

        /// <summary>
        /// Фамилия менеджера
        /// </summary>
        [Required] public string SecondName { get; set; }

        [Required] public string Email { get; set; }
    }
}
