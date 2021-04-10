using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.User;

namespace WorldYachts.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly IEfRepository<Data.Entities.Customer> _repository; 
        public CustomerService(IEfRepository<Data.Entities.Customer> repository)
        {
            _repository = repository;
        }
        
        public async Task<ServiceResponse<Data.Entities.Customer>> Add(Data.Entities.Customer customer)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntity(customer))
            {
                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = false,
                    Data = customer,
                    Message = "Customer already exist.",
                    Time = now
                };
            }

            var addedCustomer = await _repository.Add(customer);

            return new ServiceResponse<Data.Entities.Customer>()
            {
                IsSuccess = true,
                Data = addedCustomer,
                Message = $"Customer (id:{addedCustomer.Id} First name:{addedCustomer.FirstName} Second Name:{addedCustomer.SecondName}) added",
                Time = now
            };
        }

        public IEnumerable<Data.Entities.Customer> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<ServiceResponse<Data.Entities.Customer>> GetById(int id)
        {
            var customer = await _repository.GetById(id);

            return new ServiceResponse<Data.Entities.Customer>()
            {
                IsSuccess = customer != null,
                Data = customer,
                Message = customer != null ? $"Got customer by id: {id}" : "Customer not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.Customer>> Update(int id, Data.Entities.Customer customer)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntity(customer))
            {
                return new ServiceResponse<Data.Entities.Customer>()
                {
                    IsSuccess = false,
                    Data = customer,
                    Message = $"Customer already exist.",
                    Time = now
                };
            }

            var updatedCustomer = await _repository.Update(id, customer);

            return new ServiceResponse<Data.Entities.Customer>()
            {
                IsSuccess = true,
                Data = updatedCustomer,
                Message = $"Customer (id:{updatedCustomer.Id} First name:{updatedCustomer.FirstName} Second Name:{updatedCustomer.SecondName}) updated",
                Time = now
            };
        }

        public async Task<ServiceResponse<Data.Entities.Customer>> Delete(int id)
        {
            var now = DateTime.UtcNow;
            var deletedCustomer = await _repository.Delete(id);

            return new ServiceResponse<Data.Entities.Customer>()
            {
                IsSuccess = deletedCustomer != null,
                Data = deletedCustomer,
                Message = deletedCustomer != null ? $"Customer (id:{id}) deleted" : "Customer not found",
                Time = now,
            };
        }

        public async Task<bool> IsIdenticalEntity(Data.Entities.Customer customer)
        {
            //Проверка по номеру документов и номеру телефона
            if (await _repository.Find(c=>(c.IdNumber == customer.IdNumber 
                                     || c.Phone == customer.Phone )
                                    && c.Id != customer.Id) != null)
            {
                return true;
            }

            return false;
        }
    }
}
