using Microsoft.EntityFrameworkCore;
using Persistence.DomainModels;

namespace Persistence.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Tier> Tiers { get; set; }

        Task<int> SaveChanges();
    }
}