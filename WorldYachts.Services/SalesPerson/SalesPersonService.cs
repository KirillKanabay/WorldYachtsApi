using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;

namespace WorldYachts.Services.SalesPerson
{
    public class SalesPersonService : ISalesPersonService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;

        public SalesPersonService(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> AddAsync(Data.Entities.SalesPerson salesPerson)
        {
            var now = DateTime.UtcNow;
            try
            {
                var addedSalesPerson = (await _db.SalesPersons.AddAsync(salesPerson)).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = true,
                    Data = addedSalesPerson,
                    Message = $"SalesPerson (id:{addedSalesPerson.Id} First name:{addedSalesPerson.FirstName} " +
                              $"Second Name:{addedSalesPerson.SecondName}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = false,
                    Data = salesPerson,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public IEnumerable<Data.Entities.SalesPerson> GetAll()
        {
            return _db.SalesPersons
                .Include(sp => sp.Orders);
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> GetByIdAsync(int id)
        {
            var salesPerson = await _db.SalesPersons
                .Include(sp => sp.Orders)
                .FirstOrDefaultAsync(sp => sp.Id == id);

            return new ServiceResponse<Data.Entities.SalesPerson>()
            {
                IsSuccess = salesPerson != null,
                Data = salesPerson,
                Message = salesPerson != null ? $"Got sales person by id: {id}" : "Sales person not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> UpdateAsync(int id,
            Data.Entities.SalesPerson salesPerson)
        {
            var now = DateTime.UtcNow;
            try
            {
                var updatedSalesPerson = await _db.SalesPersons.FindAsync(id);
                updatedSalesPerson = _mapper.Map(salesPerson, updatedSalesPerson);
                _db.SalesPersons.Update(updatedSalesPerson);
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = true,
                    Data = updatedSalesPerson,
                    Message = $"Sales person (id:{updatedSalesPerson.Id} First name:{updatedSalesPerson.FirstName} " +
                              $"Second Name:{updatedSalesPerson.SecondName}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = false,
                    Data = salesPerson,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Data.Entities.SalesPerson>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var salesPerson = await _db.SalesPersons.FindAsync(id);

                var deletedSalesPerson = _db.SalesPersons.Remove(salesPerson).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = deletedSalesPerson != null,
                    Data = deletedSalesPerson,
                    Message = deletedSalesPerson != null
                        ? $"Sales person (id:{id}, {deletedSalesPerson.FirstName} {deletedSalesPerson.SecondName}) deleted"
                        : "Sales person not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Data.Entities.SalesPerson>()
                {
                    IsSuccess = false,
                    Data = null,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

    }
}