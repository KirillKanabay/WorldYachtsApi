using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Services.Models.Authenticate;

namespace WorldYachts.Services.User
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        Task<Data.Entities.User> Add(Data.Entities.User user);
        IEnumerable<Data.Entities.User> GetAll();
        Data.Entities.User GetById(int id);
        Task<bool> IsIdenticalEntity(Data.Entities.User user);
    }
}
