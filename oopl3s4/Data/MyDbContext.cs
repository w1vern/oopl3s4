using Microsoft.EntityFrameworkCore;

namespace oopl3s4.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<Models.Employee> Employees { get; set; } = null!;
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Employee>().HasData(
                new Models.Employee { age = 10, Id = 1, Name = "Grigory"}
                );
        }
    }
}