using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<District> Districts { get; set; }
        public DbSet<Report> Reports { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure one-to-many relationship
            modelBuilder.Entity<District>()
                .HasMany(d => d.Reports)
                .WithOne()
                .HasForeignKey(r => r.DisctrictId);

            // Optional: Configure cascading delete behavior
            modelBuilder.Entity<District>()
                .HasMany(d => d.Reports)
                .WithOne()
                .HasForeignKey(r => r.DisctrictId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}