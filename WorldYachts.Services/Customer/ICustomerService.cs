using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WorldYachts.Services.Customer
{
    public interface ICustomerService
    {
        Task<Data.Models.Customer> Add(Data.Models.Customer customer);
        IEnumerable<WorldYachts.Data.Models.Customer> GetAll();
        Task<Data.Models.Customer> GetById(int id);
    }
}
