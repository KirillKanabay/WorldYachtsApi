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
        Task<ServiceResponse<Data.Entities.Customer>> AddAsync(Data.Entities.Customer customer);
        IEnumerable<Data.Entities.Customer> GetAll();
        Task<ServiceResponse<Data.Entities.Customer>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.Customer>> UpdateAsync(int id, Data.Entities.Customer customer);
        Task<ServiceResponse<Data.Entities.Customer>> DeleteAsync(int id);
        Task<bool> IsIdenticalEntityAsync(Data.Entities.Customer customer);
    }
}
