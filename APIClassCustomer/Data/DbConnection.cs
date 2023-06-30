using APIClassCustomer.Model;
using Microsoft.EntityFrameworkCore;

namespace APIClassCustomer.Data
{
    public class DbConnection:DbContext
    {
        public DbConnection(DbContextOptions options) : base(options) { }
        public DbSet<Class> Class { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
