using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Models;

namespace WorldYachts.Services.Boat
{
    public interface IBoatService
    {
        Task<ServiceResponse<Data.Entities.Boat>> Add(Data.Entities.Boat boat);
        IEnumerable<Data.Entities.Boat> GetAll();
        Task<ServiceResponse<Data.Entities.Boat>> GetById(int id);
        Task<ServiceResponse<Data.Entities.Boat>> Update(int id, Data.Entities.Boat boat);
        Task<ServiceResponse<Data.Entities.Boat>> Delete(int id);
        Task<bool> IsIdenticalEntity(Data.Entities.Boat boat);
    }
}
