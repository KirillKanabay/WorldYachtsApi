using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.SalesPerson
{
    public interface ISalesPersonService
    {
        Task<ServiceResponse<Data.Entities.SalesPerson>> AddAsync(Data.Entities.SalesPerson salesPerson);
        IEnumerable<Data.Entities.SalesPerson> GetAll();
        Task<ServiceResponse<Data.Entities.SalesPerson>> GetByIdAsync(int id);
        Task<ServiceResponse<Data.Entities.SalesPerson>> UpdateAsync(int id, Data.Entities.SalesPerson salesPerson);
        Task<ServiceResponse<Data.Entities.SalesPerson>> DeleteAsync(int id);
    }
}
