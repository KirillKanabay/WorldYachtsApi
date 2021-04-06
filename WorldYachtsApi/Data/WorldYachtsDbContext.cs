using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorldYachtsApi.Entities;
using Partner = WorldYachtsApi.Models.Partner;

namespace WorldYachtsApi.Data
{
    public class WorldYachtsDbContext:DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<User> Users { get; set; }
        public WorldYachtsDbContext(DbContextOptions<WorldYachtsDbContext> options):base(options)
        {
            
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
