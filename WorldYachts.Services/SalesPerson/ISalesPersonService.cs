using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WorldYachts.Data.Models;

namespace WorldYachts.Services.SalesPerson
{
    public interface ISalesPersonService
    {
        Task<Data.Models.SalesPerson> Add(Data.Models.SalesPerson salesPerson);
        IEnumerable<WorldYachts.Data.Models.SalesPerson> GetAll();
        Task<Data.Models.SalesPerson> GetById(int id);
    }
}
