using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.AccessoryToBoat
{
    public interface IAccessoryToBoatService
    {
        Task<ServiceResponse<Data.Entities.AccessoryToBoat>> AddAsync(Data.Entities.AccessoryToBoat accessoryToBoat);
        IEnumerable<Data.Entities.AccessoryToBoat> GetAll();
        Task<ServiceResponse<Data.Entities.AccessoryToBoat>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.AccessoryToBoat>> UpdateAsync(int id, Data.Entities.AccessoryToBoat accessoryToBoat);
        Task<ServiceResponse<Data.Entities.AccessoryToBoat>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Data.Entities.AccessoryToBoat accessoryToBoat);
    }
}
