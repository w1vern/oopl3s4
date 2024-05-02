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
        public DbSet<oopl3s4.Models.Artisan> Artisan { get; set; } = default!;
        public DbSet<oopl3s4.Models.Craft> Craft { get; set; } = default!;
        public DbSet<oopl3s4.Models.Relation> Relation { get; set; } = default!;
    }
}