using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Services.Models;
using WorldYachts.Services.Models.Authenticate;

namespace WorldYachts.Services.SalesPerson
{
    public interface ISalesPersonService
    {
        Task<Data.Entities.SalesPerson> Add(Data.Entities.SalesPerson salesPerson);
        IEnumerable<Data.Entities.SalesPerson> GetAll();
        Task<Data.Entities.SalesPerson> GetById(int id);
        Task<AuthenticateResponse> Register(SalesPersonModel salesPersonModel);
        Task<bool> IsIdenticalEntity(Data.Entities.SalesPerson salesPerson);
    }
}
