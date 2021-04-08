using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachtsApi.Models.Authenticate;

namespace WorldYachts.Services.User
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<Data.Models.User> Add(Data.Models.User user);
        IEnumerable<Data.Models.User> GetAll();
        Data.Models.User GetById(int id);
        Task<bool> IsIdenticalEntity(Data.Models.User user);
    }
}
