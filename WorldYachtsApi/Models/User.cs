using System.ComponentModel.DataAnnotations;

namespace WorldYachtsApi.Models
{
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required] public int Id { get; set; }
        /// <summary>
        /// Является ли предмет удаленным
        /// </summary>
        [Required] public bool IsDeleted { get; set; }
        /// <summary>
        /// Тип пользователя
        /// </summary>
        [Required] public int TypeUser { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [Required] public string Login { get; set; }
        /// <summary>
        /// Id пользователя
        /// </summary>
        [Required] public int UserId { get; set; }
    }
}
