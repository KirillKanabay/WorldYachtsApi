using System.Collections.Generic;
using System.Threading.Tasks;
using Contract = WorldYachts.Data.Entities.Contract;

namespace WorldYachts.Services.Contracts
{
    public interface IContractService
    {
        Task<ServiceResponse<Contract>> AddAsync(Contract contract);
        IEnumerable<Contract> GetAll();
        Task<ServiceResponse<Contract>> GetByIdAsync(int id);
        Task<ServiceResponse<Contract>> UpdateAsync(int id, Contract contract);
        Task<ServiceResponse<Contract>> DeleteAsync(int id);
    }
}
