using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorldYachtsApi.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
