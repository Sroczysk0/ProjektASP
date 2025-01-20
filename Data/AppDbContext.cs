using Microsoft.EntityFrameworkCore;
using Projekt01.Models;

namespace Projekt01.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<game> game { get; set; }
        public DbSet<genre> genre { get; set; }
        public DbSet<publisher> publisher { get; set; }
        public DbSet<game_publisher> game_publisher { get; set; }
        public DbSet<game_platform> game_platform { get; set; }
        public DbSet<platform> platform { get; set; }
        public DbSet<region> region { get; set; }
        public DbSet<region_sales> region_sales { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<game_platform>()
                .ToTable("game_platform")
                .HasOne(gp => gp.platform)
                .WithMany()
                .HasForeignKey(gp => gp.platform_id);

            modelBuilder.Entity<game>()
                .ToTable("game")
                .HasOne(g => g.genre)
                .WithMany()
                .HasForeignKey(g => g.genre_id);

            modelBuilder.Entity<genre>().ToTable("genre");

            modelBuilder.Entity<publisher>().ToTable("publisher");

            modelBuilder.Entity<game_publisher>().ToTable("game_publisher");

            modelBuilder.Entity<platform>().ToTable("platform");

            modelBuilder.Entity<region_sales>()
                .ToTable("region_sales")
                .HasKey(rs => new { rs.region_id, rs.game_platform_id });

            modelBuilder.Entity<region>().ToTable("region");
        }

    }

    public class region_sales
    {
        public int region_id { get; set; }
        public int game_platform_id { get; set; }
        public int num_sales { get; set; }
    }
}