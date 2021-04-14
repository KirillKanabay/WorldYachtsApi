using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Accessories
{
    public interface IAccessoryService
    {
        Task<ServiceResponse<Accessory>> AddAsync(Accessory accessory);
        Task<ServiceResponse<Accessory>> GetByIdAsync(int id);
        IEnumerable<Accessory> GetAll();
        Task<ServiceResponse<Accessory>> UpdateAsync(int id, Accessory accessory);
        Task<ServiceResponse<Accessory>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Accessory accessory);
    }
}
