using Microsoft.EntityFrameworkCore;
using TheHealthCare.Models.Enitities;

namespace TheHealthCare.DATA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<patients> patients{ get; set; }
        public DbSet<Doctor> doctors{ get; set; }
        public DbSet<Appointment> Appointments{ get; set; }
        public DbSet<User> Users { get; set; }
    }
}
