using Domain.DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Context
{
    internal class UserProjectConfiguration : IEntityTypeConfiguration<ProjectUser>
    {
        public void Configure(EntityTypeBuilder<ProjectUser> builder)
        {
            builder.ToTable("ProjectUsers");

            builder.HasKey(up => new { up.UserId, up.ProjectId });

            builder.HasOne(up => up.User)
            .WithMany(u => u.SupportedProjects)
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(up => up.Project)
            .WithMany(p => p.Supporters)
            .HasForeignKey(up => up.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
