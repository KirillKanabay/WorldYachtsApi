using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.Models.Authenticate;

namespace WorldYachts.Services.User
{
    public interface IUserService
    {
        Task<ServiceResponse<AuthenticateResponse>> AuthenticateAsync(AuthenticateRequest model);
        Task<ServiceResponse<Data.Entities.User>> AddAsync(Data.Entities.User user);
        IEnumerable<Data.Entities.User> GetAll();
        Task<ServiceResponse<Data.Entities.User>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.User>> UpdateAsync(int id, Data.Entities.User user);
        Task<ServiceResponse<Data.Entities.User>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Data.Entities.User user);
    }
}
