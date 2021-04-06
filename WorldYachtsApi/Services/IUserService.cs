using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldYachtsApi.Entities;
using WorldYachtsApi.Models;
using WorldYachtsApi.Models.Authenticate;

namespace WorldYachtsApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<AuthenticateResponse> Register(UserModel userModel);
        IEnumerable<User> GetAll();
        User GetById(int id);
    }
}
