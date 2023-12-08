using Domain.DomainModels.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Abstract
{
    public interface IApplicationDbContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Tier> Tiers { get; set; }
        DbSet<Image> Images { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }

        Task<int> SaveChanges();
    }
}
