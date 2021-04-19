using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Order
{
    public interface IOrderService
    {
        Task<ServiceResponse<Data.Entities.Order>> AddAsync(Data.Entities.Order order);
        Task<ServiceResponse<Data.Entities.Order>> GetByIdAsync(int id);
        IEnumerable<Data.Entities.Order> GetAll();
        Task<ServiceResponse<Data.Entities.Order>> UpdateAsync(int id, Data.Entities.Order order);
        Task<ServiceResponse<Data.Entities.Order>> DeleteAsync(int id);
    }
}
