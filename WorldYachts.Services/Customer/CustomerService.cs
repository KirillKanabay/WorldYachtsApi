using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;

namespace WorldYachts.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly WorldYachtsDbContext _dbContext;
        public CustomerService(WorldYachtsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Data.Models.Customer> Add(Data.Models.Customer customer)
        {
            var addedCustomer = await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return addedCustomer.Entity;
        }

        public IEnumerable<Data.Models.Customer> GetAll()
        {
            return _dbContext.Customers;
        }

        public async Task<Data.Models.Customer> GetById(int id)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
