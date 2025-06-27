using Microsoft.EntityFrameworkCore;
using GG.Models;

namespace GG.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<RestaurantBrand> RestaurantBrands { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemAvailability> ItemAvailabilities { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
