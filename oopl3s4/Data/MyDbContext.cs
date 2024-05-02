using Microsoft.EntityFrameworkCore;
using oopl3s4.Models;

namespace oopl3s4.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Emails>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Models.IsInheritance>().HasIndex(u => u.ArtisanID).IsUnique();
        }
        public DbSet<oopl3s4.Models.Artisan> Artisan { get; set; } = default!;
        public DbSet<oopl3s4.Models.Craft> Craft { get; set; } = default!;
        public DbSet<oopl3s4.Models.Relation> Relation { get; set; } = default!;
        public DbSet<oopl3s4.Models.Emails> Emails { get; set; } = default!;
        public DbSet<oopl3s4.Models.IsInheritance> IsInheritance { get; set; } = default!;
    }
}