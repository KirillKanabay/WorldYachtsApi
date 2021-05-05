using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;

namespace WorldYachts.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;

        public CustomerService(WorldYachtsDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Data.Entities.Customer>> AddAsync(Data.Entities.Customer customer)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(customer))
                {
                    return new ServiceResponse<Data.Entities.Customer>()
                    {
                        IsSuccess = false,
                        Data = customer,
                        Message = "Customer already exist.",
                        Time = now
                    };
                }

                var addedCustomer = (await _db.Customers.AddAsync(customer)).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = true,
                    Data = addedCustomer,
                    Message = $"Customer (id:{addedCustomer.Id} First name:{addedCustomer.FirstName} Second Name:{addedCustomer.SecondName}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = false,
                    Data = customer,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
            
        }

        public IEnumerable<Data.Entities.Customer> GetAll()
        {
            return _db.Customers.Include(c => c.Orders);
        }

        public async Task<ServiceResponse<Data.Entities.Customer>> GetByIdAsync(int id)
        {
            var customer = await _db.Customers.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);

            return new ServiceResponse<Data.Entities.Customer>()
            {
                IsSuccess = customer != null,
                Data = customer,
                Message = customer != null ? $"Got customer by id: {id}" : "Customer not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.Customer>> UpdateAsync(int id, Data.Entities.Customer customer)
        {
            var now = DateTime.UtcNow;
            try
            {
                if (await IsIdenticalEntityAsync(customer))
                {
                    return new ServiceResponse<Data.Entities.Customer>()
                    {
                        IsSuccess = false,
                        Data = customer,
                        Message = $"Customer already exist.",
                        Time = now
                    };
                }

                var updatedCustomer = await _db.Customers.FindAsync(id);
                updatedCustomer = _mapper.Map(customer, updatedCustomer);
                _db.Customers.Update(updatedCustomer);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = true,
                    Data = updatedCustomer,
                    Message =
                        $"Customer (id:{updatedCustomer.Id} First name:{updatedCustomer.FirstName} Second Name:{updatedCustomer.SecondName}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = false,
                    Data = customer,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Data.Entities.Customer>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var customer = await _db.Customers.FindAsync(id);

                var deletedCustomer = _db.Customers.Remove(customer).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = deletedCustomer != null,
                    Data = deletedCustomer,
                    Message = deletedCustomer != null ? $"Customer (id:{id}) deleted" : "Customer not found",
                    Time = now,
                };

            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
            
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.Customer customer)
        {
            //Проверка по номеру документов и номеру телефона
            if (await _db.Customers.FirstOrDefaultAsync(c=>(c.IdNumber == customer.IdNumber 
                                     || c.Phone == customer.Phone )
                                    && c.Id != customer.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}
