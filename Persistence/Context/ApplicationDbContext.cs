using Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.DomainModels.Entities;
using Domain.DomainModels.Enums;

namespace Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
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
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            builder.ApplyConfiguration(new ProjectConfiguration());
            builder.ApplyConfiguration(new TierConfiguration());
            builder.ApplyConfiguration(new UserProjectConfiguration());
            builder.HasPostgresEnum<Status>();
            base.OnModelCreating(builder);
        }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
