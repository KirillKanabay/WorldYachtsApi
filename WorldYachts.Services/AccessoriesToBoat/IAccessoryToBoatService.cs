using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.AccessoriesToBoat
{
    public interface IAccessoryToBoatService
    {
        Task<ServiceResponse<AccessoryToBoat>> AddAsync(AccessoryToBoat accessoryToBoat);
        IEnumerable<AccessoryToBoat> GetAll();
        Task<ServiceResponse<AccessoryToBoat>> GetByIdAsync(int id);
        Task<ServiceResponse<AccessoryToBoat>> UpdateAsync(int id, AccessoryToBoat accessoryToBoat);
        Task<ServiceResponse<AccessoryToBoat>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(AccessoryToBoat accessoryToBoat);
    }
}
