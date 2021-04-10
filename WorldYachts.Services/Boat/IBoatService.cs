using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Models;

namespace WorldYachts.Services.Boat
{
    public interface IBoatService
    {
        Task<Data.Entities.Boat> Add(BoatModel boatModel);
        IEnumerable<Data.Entities.Boat> GetAll();
        Task<Data.Entities.Boat> GetById(int id);
        Task<Data.Entities.Boat> Update(int id, BoatModel boat);
        Task<Data.Entities.Boat> Delete(int id);
        Task<bool> IsIdenticalEntity(Data.Entities.Boat boat);
    }
}
