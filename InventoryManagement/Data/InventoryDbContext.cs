using InventoryManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<products> products { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
