using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Services.Authenticate;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;
using WorldYachts.Services.User;

namespace WorldYachts.Services.SalesPerson
{
    public class SalesPersonService:ISalesPersonService
    {
        private readonly IEfRepository<Data.Entities.SalesPerson> _repository;
        public SalesPersonService(IEfRepository<Data.Entities.SalesPerson> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> AddAsync(Data.Entities.SalesPerson salesPerson)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntityAsync(salesPerson))
            {
                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = false,
                    Data = salesPerson,
                    Message = "Sales person already exist.",
                    Time = now
                };
            }

            var addedSalesPerson = await _repository.Add(salesPerson);

            return new ServiceResponse<Data.Entities.SalesPerson>()
            {
                IsSuccess = true,
                Data = addedSalesPerson,
                Message = $"SalesPerson (id:{addedSalesPerson.Id} First name:{addedSalesPerson.FirstName} " +
                          $"Second Name:{addedSalesPerson.SecondName}) added",
                Time = now
            };
        }

        public IEnumerable<Data.Entities.SalesPerson> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> GetByIdAsync(int id)
        {
            var salesPerson = await _repository.GetById(id);

            return new ServiceResponse<Data.Entities.SalesPerson>()
            {
                IsSuccess = salesPerson != null,
                Data = salesPerson,
                Message = salesPerson != null ? $"Got sales person by id: {id}" : "Sales person not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> UpdateAsync(int id, Data.Entities.SalesPerson salesPerson)
        {
            var now = DateTime.UtcNow;
            if (await IsIdenticalEntityAsync(salesPerson))
            {
                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = false,
                    Data = salesPerson,
                    Message = $"Sales person already exist.",
                    Time = now
                };
            }

            var updatedSalesPerson = await _repository.Update(id, salesPerson);

            return new ServiceResponse<Data.Entities.SalesPerson>()
            {
                IsSuccess = true,
                Data = updatedSalesPerson,
                Message = $"Sales person (id:{updatedSalesPerson.Id} First name:{updatedSalesPerson.FirstName} " +
                          $"Second Name:{updatedSalesPerson.SecondName}) updated",
                Time = now
            };
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            var deletedSalesPerson = await _repository.Delete(id);

            return new ServiceResponse<Data.Entities.SalesPerson>()
            {
                IsSuccess = deletedSalesPerson != null,
                Data = deletedSalesPerson,
                Message = deletedSalesPerson != null ? $"Sales person (id:{id}) deleted" : "Sales person not found",
                Time = now,
            };
        }

        public async Task<bool> IsIdenticalEntityAsync(Data.Entities.SalesPerson salesPerson)
        {
            return true;
        }
    }
}
