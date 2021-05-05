using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
        public Task<ServiceResponse<Contract>> AddAsync(Contract contract)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Contract> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<Contract>> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<Contract>> UpdateAsync(int id, Contract contract)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResponse<Contract>> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsIdenticalEntityAsync(Contract contract)
        {
            throw new System.NotImplementedException();
        }
    }
}
