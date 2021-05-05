using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Boats
{
    public interface IBoatService
    {
        Task<ServiceResponse<Boat>> AddAsync(Boat boat);
        IEnumerable<Boat> GetAll();
        Task<ServiceResponse<Boat>> GetByIdAsync(int id);
        Task<ServiceResponse<Boat>> UpdateAsync(int id, Boat boat);
        Task<ServiceResponse<Boat>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Boat boat);
    }
}
