using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.BoatWoods
{
    public interface IBoatWoodService
    {
        Task<ServiceResponse<Data.Entities.BoatWood>> AddAsync(Data.Entities.BoatWood boatWood);
        IEnumerable<Data.Entities.BoatWood> GetAll();
        Task<ServiceResponse<Data.Entities.BoatWood>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.BoatWood>> UpdateAsync(int id, Data.Entities.BoatWood boatWood);
        Task<ServiceResponse<Data.Entities.BoatWood>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Data.Entities.BoatWood boatWood);
    }
}
