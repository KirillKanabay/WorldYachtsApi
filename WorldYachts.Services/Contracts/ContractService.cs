using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;
using WorldYachts.Data.Entities;

namespace WorldYachts.Services.Contracts
{
    public class ContractService:IContractService
    {
        private readonly WorldYachtsDbContext _db;
        private readonly IMapper _mapper;

        public ContractService(WorldYachtsDbContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Contract>> AddAsync(Contract contract)
        {
            var now = DateTime.UtcNow;
            try
            {
                var addedContract = (await _db.Contracts.AddAsync(contract)).Entity;
                await _db.SaveChangesAsync();
                return new ServiceResponse<Contract>()
                {
                    IsSuccess = true,
                    Data = addedContract,
                    Message = $"Contract (id:{contract.Id}) added",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Contract>()
                {
                    IsSuccess = false,
                    Data = contract,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public IEnumerable<Contract> GetAll()
        {
            return _db.Contracts.Include(c => c.Order);
        }

        public async Task<ServiceResponse<Contract>> GetByIdAsync(int id)
        {
            var contract = await _db.Contracts
                .Include(c => c.Order)
                .FirstOrDefaultAsync(a => a.Id == id);

            return new ServiceResponse<Contract>()
            {
                IsSuccess = contract != null,
                Data = contract,
                Message = contract != null ? $"Got contract by id: {id}" : "Accessory not found",
                Time = DateTime.UtcNow,
            };
        }

        public async Task<ServiceResponse<Contract>> UpdateAsync(int id, Contract contract)
        {
            var now = DateTime.UtcNow;
            try
            {
                var updatedContract = await _db.Contracts.FindAsync(id);
                updatedContract = _mapper.Map(contract, updatedContract);
                updatedContract.Id = id;

                await _db.SaveChangesAsync();

                return new ServiceResponse<Contract>()
                {
                    IsSuccess = true,
                    Data = updatedContract,
                    Message = $"Contract (id:{updatedContract.Id}) updated",
                    Time = now
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Contract>()
                {
                    IsSuccess = false,
                    Data = contract,
                    Message = $"{e.Message} {Environment.NewLine}" +
                              $"{e.InnerException?.Message}",
                    Time = now
                };
            }
        }

        public async Task<ServiceResponse<Contract>> DeleteAsync(int id)
        {
            var now = DateTime.UtcNow;
            try
            {
                var contract = await _db.Contracts.FindAsync(id);

                var deletedContract = _db.Contracts.Remove(contract).Entity;
                await _db.SaveChangesAsync();

                return new ServiceResponse<Contract>()
                {
                    IsSuccess = deletedContract != null,
                    Data = deletedContract,
                    Message = deletedContract != null
                        ? $"Contract (id:{id}) deleted" : "Contract not found",
                    Time = now,
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<Contract>()
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
