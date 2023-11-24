using Domain.DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context
{
    internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.HasKey(x => x.Id);

            builder.HasMany(u => u.CreatedProjects)
                .WithOne(p => p.Creator)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
