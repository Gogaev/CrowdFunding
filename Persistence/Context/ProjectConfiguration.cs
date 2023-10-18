using Domain.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.HasMany(p => p.Tiers)
                .WithOne(t => t.Project)
                .HasForeignKey(fk => fk.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Title)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired();

            builder.Property(p => p.LastDay)
                .IsRequired();

            builder.Property(p => p.StartingDay)
                .IsRequired();

            builder.Property(p => p.RequiredMoney)
                .HasPrecision(15, 2)
                .IsRequired();

            builder.Property(p => p.InvestedMoney)
                .HasPrecision(15, 2);
        }
    }
}
