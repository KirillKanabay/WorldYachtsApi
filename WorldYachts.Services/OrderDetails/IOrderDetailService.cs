using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.OrderDetails
{
    public interface IOrderDetailService
    {
        Task<ServiceResponse<Data.Entities.OrderDetail>> AddAsync(Data.Entities.OrderDetail orderDetail);
        Task<ServiceResponse<Data.Entities.OrderDetail>> GetByIdAsync(int id);
        IEnumerable<Data.Entities.OrderDetail> GetAll();
        Task<ServiceResponse<Data.Entities.OrderDetail>> UpdateAsync(int id, Data.Entities.OrderDetail orderDetail);
        Task<ServiceResponse<Data.Entities.OrderDetail>> DeleteAsync(int id);
    }
}
