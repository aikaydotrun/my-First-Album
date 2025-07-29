using Microsoft.EntityFrameworkCore;
using zibabeachresortAPI.Models.Entities;

namespace zibabeachresortAPI.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public  DbSet<Employee> employees{ get; set; }
    }
}
