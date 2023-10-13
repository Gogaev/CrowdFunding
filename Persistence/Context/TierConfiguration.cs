﻿using Domain.DomainModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    internal class TierConfiguration : IEntityTypeConfiguration<Tier>
    {
        public void Configure(EntityTypeBuilder<Tier> builder)
        {
            builder.ToTable("Tiers");

            builder.Property(p => p.TierName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(p => p.RequiredMoney)
                .HasPrecision(15, 2)
                .IsRequired();

            builder.Property(p => p.Benefit)
                .HasMaxLength(30);

            builder.Property(p => p.IsReached)
                .IsRequired();
        }
    }
}