using Microsoft.EntityFrameworkCore;
using TheRealEstate.Models.Entities;

namespace TheRealEstate.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Agent> Agents { get; set; }
    }
}
