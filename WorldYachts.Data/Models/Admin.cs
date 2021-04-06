using System.ComponentModel.DataAnnotations;

namespace WorldYachts.Data.Models
{
    public class Admin
    {
        /// <summary>
        /// Идентификатор администратора
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя администратора
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия администратора
        /// </summary>
        public string SecondName { get; set; }
    }
}
