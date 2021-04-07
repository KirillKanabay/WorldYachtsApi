using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldYachts.Data;

namespace WorldYachts.Services.SalesPerson
{
    public class SalesPersonService:ISalesPersonService
    {
        private readonly WorldYachtsDbContext _dbContext;
        public SalesPersonService(WorldYachtsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Data.Models.SalesPerson> Add(Data.Models.SalesPerson salesPerson)
        {
            var addedSalesPerson = await _dbContext.SalesPersons.AddAsync(salesPerson);
            await _dbContext.SaveChangesAsync();
            
            return addedSalesPerson.Entity;
        }

        public IEnumerable<Data.Models.SalesPerson> GetAll()
        {
            return _dbContext.SalesPersons;
        }

        public async Task<Data.Models.SalesPerson> GetById(int id)
        {
            return await _dbContext.SalesPersons.FirstOrDefaultAsync(sp => sp.Id == id);
        }
    }
}
