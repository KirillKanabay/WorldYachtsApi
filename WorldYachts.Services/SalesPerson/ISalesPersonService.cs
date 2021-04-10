using System.Collections.Generic;
using System.Threading.Tasks;

namespace WorldYachts.Services.SalesPerson
{
    public interface ISalesPersonService
    {
        Task<ServiceResponse<Data.Entities.SalesPerson>> Add(Data.Entities.SalesPerson salesPerson);
        IEnumerable<Data.Entities.SalesPerson> GetAll();
        Task<ServiceResponse<Data.Entities.SalesPerson>> GetById(int id);
        Task<ServiceResponse<Data.Entities.SalesPerson>> Update(int id, Data.Entities.SalesPerson salesPerson);
        Task<ServiceResponse<Data.Entities.SalesPerson>> Delete(int id);
        Task<bool> IsIdenticalEntity(Data.Entities.SalesPerson salesPerson);
    }
}
