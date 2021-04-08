using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Models;
using WorldYachts.Services.Models;
using WorldYachtsApi.Models.Authenticate;

namespace WorldYachts.Services.SalesPerson
{
    public interface ISalesPersonService
    {
        Task<Data.Models.SalesPerson> Add(Data.Models.SalesPerson salesPerson);
        IEnumerable<WorldYachts.Data.Models.SalesPerson> GetAll();
        Task<Data.Models.SalesPerson> GetById(int id);
        Task<AuthenticateResponse> Register(SalesPersonModel salesPersonModel);
    }
}
