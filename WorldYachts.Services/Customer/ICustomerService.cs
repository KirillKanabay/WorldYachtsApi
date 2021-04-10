using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;

namespace WorldYachts.Services.Customer
{
    public interface ICustomerService
    {
        Task<Data.Entities.Customer> Add(Data.Entities.Customer customer);
        IEnumerable<Data.Entities.Customer> GetAll();
        Task<Data.Entities.Customer> GetById(int id);
        Task<AuthenticateResponse> Register(CustomerModel customerModel);
        Task<bool> IsIdenticalEntity(Data.Entities.Customer customer);
    }
}
