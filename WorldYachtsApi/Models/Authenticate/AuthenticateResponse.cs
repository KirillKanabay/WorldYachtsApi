using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachtsApi.Entities;

namespace WorldYachtsApi.Models.Authenticate
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Patronymic = user.Patronymic;
            Username = user.Username;
            Email = user.Email;
            Token = token;
        }
    }
}
