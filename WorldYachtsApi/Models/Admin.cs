using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
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

        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
    }
}
