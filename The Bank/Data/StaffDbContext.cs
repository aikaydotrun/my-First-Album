using Microsoft.EntityFrameworkCore;
using TheBank.Models.Entities;

namespace TheBank.Data
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options)
        {
        }

        public DbSet<Staff>  Staff{ get; set; }
    }
    
    
}
