using Domain.DomainModels;
using Microsoft.EntityFrameworkCore;

namespace Domain.Abstract
{
    public interface IApplicationDbContext
    {
        DbSet<Project> Projects { get; set; }
        DbSet<Tier> Tiers { get; set; }

        Task<int> SaveChanges();
    }
}
