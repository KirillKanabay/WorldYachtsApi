using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;

namespace WorldYachts.Services.Customer
{
    public interface ICustomerService
    {
        Task<ServiceResponse<Data.Entities.Customer>> Add(Data.Entities.Customer customer);
        IEnumerable<Data.Entities.Customer> GetAll();
        Task<ServiceResponse<Data.Entities.Customer>> GetById(int id);
        Task<ServiceResponse<Data.Entities.Customer>> Update(int id, Data.Entities.Customer customer);
        Task<ServiceResponse<Data.Entities.Customer>> Delete(int id);
        Task<bool> IsIdenticalEntity(Data.Entities.Customer boat);
    }
}
