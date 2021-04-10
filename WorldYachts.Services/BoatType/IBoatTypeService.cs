using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SolarCoffee.Services;
using WorldYachts.Services.Models;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.BoatType
{
    public interface IBoatTypeService
    {
        Task<ServiceResponse<Data.Entities.BoatType>> Add(Data.Entities.BoatType boatType);
        IEnumerable<Data.Entities.BoatType> GetAll();
        Task<ServiceResponse<Data.Entities.BoatType>> GetById(int id);
        Task<ServiceResponse<Data.Entities.BoatType>> Update(int id, Data.Entities.BoatType boatType); 
        Task<ServiceResponse<Data.Entities.BoatType>> Delete(int id);
    }
}
