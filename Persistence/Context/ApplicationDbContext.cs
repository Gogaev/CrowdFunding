using Domain.Abstract;
using Domain.DomainModels;
using Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
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
