using Microsoft.EntityFrameworkCore;
using Npgsql;
using Persistence.DomainModels;

namespace Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        static ApplicationDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Status>();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<Status>();
            base.OnModelCreating(builder);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Tier> Tiers { get; set; }

    }
}
