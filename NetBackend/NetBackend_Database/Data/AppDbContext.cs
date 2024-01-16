using Microsoft.EntityFrameworkCore;
using NetBackend_Database.Model;

namespace NetBackend_Database
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }

        public DbSet<Customer> Customers { get; set; }
    }
}
