using Microsoft.EntityFrameworkCore;
using WorldYachts.Data.Models;

namespace WorldYachts.Data
{
    public class WorldYachtsDbContext:DbContext
    {
        public WorldYachtsDbContext(DbContextOptions<WorldYachtsDbContext> options):base(options)
        {
            
        }
        public DbSet<Partner> Partners { get; set; }
    }
}
