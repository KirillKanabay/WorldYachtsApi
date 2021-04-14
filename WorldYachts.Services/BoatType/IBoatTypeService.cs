using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Models;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.BoatType
{
    public interface IBoatTypeService
    {
        Task<ServiceResponse<Data.Entities.BoatType>> AddAsync(Data.Entities.BoatType boatType);
        IEnumerable<Data.Entities.BoatType> GetAll();
        Task<ServiceResponse<Data.Entities.BoatType>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.BoatType>> UpdateAsync(int id, Data.Entities.BoatType boatType); 
        Task<ServiceResponse<Data.Entities.BoatType>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Data.Entities.BoatType boatType);
    }
}
