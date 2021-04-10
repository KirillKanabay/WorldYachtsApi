using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.Models.Authenticate;

namespace WorldYachts.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthenticateResponse>> Authenticate(AuthenticateRequest model);
        Task<ServiceResponse<Data.Entities.User>> Add(Data.Entities.User user);
        IEnumerable<Data.Entities.User> GetAll();
        Task<ServiceResponse<Data.Entities.User>> GetById(int id);
        Task<ServiceResponse<Data.Entities.User>> Update(int id, Data.Entities.User user);
        Task<ServiceResponse<Data.Entities.User>> Delete(int id);
        Task<bool> IsIdenticalEntity(Data.Entities.User user);
    }
}
