using Microsoft.EntityFrameworkCore;
using TheBank.Models.Entities;

namespace TheBank.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        public DbSet<Customer>  customers { get; set; }
    }
}
