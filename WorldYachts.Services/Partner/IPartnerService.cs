using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.Partner
{
    public interface IPartnerService
    {
        Task<ServiceResponse<Data.Entities.Partner>> AddAsync(Data.Entities.Partner partner);
        IEnumerable<Data.Entities.Partner> GetAll();
        Task<ServiceResponse<Data.Entities.Partner>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.Partner>> UpdateAsync(int id, Data.Entities.Partner partner);
        Task<ServiceResponse<Data.Entities.Partner>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Data.Entities.Partner partner);
    }
}
