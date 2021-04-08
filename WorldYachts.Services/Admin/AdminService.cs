using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WorldYachts.Data;

namespace WorldYachts.Services.Admin
{
    public class AdminService:IAdminService
    {
        private readonly WorldYachtsDbContext _dbContext;
        public AdminService(WorldYachtsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Data.Models.Admin> GetAll()
        {
            return _dbContext.Admins;
        }

        public async Task<Data.Models.Admin> GetById(int id)
        {
            return await _dbContext.Admins.FindAsync(id);
        }
    }
}
