using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Models;

namespace WorldYachts.Services.Boat
{
    public interface IBoatService
    {
        Task<ServiceResponse<Data.Entities.Boat>> AddAsync(Data.Entities.Boat boat);
        IEnumerable<Data.Entities.Boat> GetAll();
        Task<ServiceResponse<Data.Entities.Boat>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.Boat>> UpdateAsync(int id, Data.Entities.Boat boat);
        Task<ServiceResponse<Data.Entities.Boat>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Data.Entities.Boat boat);
    }
}
