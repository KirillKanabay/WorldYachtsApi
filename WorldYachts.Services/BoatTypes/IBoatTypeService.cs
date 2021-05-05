using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.BoatTypes
{
    public interface IBoatTypeService
    {
        Task<ServiceResponse<BoatType>> AddAsync(BoatType boatType);
        IEnumerable<BoatType> GetAll();
        Task<ServiceResponse<BoatType>> GetByIdAsync(int id);
        Task<ServiceResponse<BoatType>> UpdateAsync(int id, BoatType boatType); 
        Task<ServiceResponse<BoatType>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(BoatType boatType);
    }
}
