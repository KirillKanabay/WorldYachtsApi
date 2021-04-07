using Microsoft.EntityFrameworkCore;
using WorldYachts.Data.Models;

namespace WorldYachts.Data
{
    public class WorldYachtsDbContext:DbContext
    {
        public DbSet<Partner> Partners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public WorldYachtsDbContext(DbContextOptions<WorldYachtsDbContext> options) : base(options)
        {
        }
    }
}
