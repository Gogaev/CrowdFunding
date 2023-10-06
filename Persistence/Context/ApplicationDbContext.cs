using Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
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

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Tier> Tiers { get; set; }
    }
}