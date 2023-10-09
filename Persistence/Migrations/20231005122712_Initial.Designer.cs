﻿// <auto-generated />
using Domain.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231005122712_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "status", new[] { "draft", "published", "finished", "expired" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Persistence.DomainModels.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("InvestedMoney")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("LastDay")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("RequiredMoney")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("StartingDay")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Status>("Status")
                        .HasColumnType("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Persistence.DomainModels.Tier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Benefit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsReached")
                        .HasColumnType("boolean");

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<decimal>("RequiredMoney")
                        .HasColumnType("numeric");

                    b.Property<string>("TierName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tiers");
                });

            modelBuilder.Entity("Persistence.DomainModels.Tier", b =>
                {
                    b.HasOne("Persistence.DomainModels.Project", "Project")
                        .WithMany("Tiers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Persistence.DomainModels.Project", b =>
                {
                    b.Navigation("Tiers");
                });
#pragma warning restore 612, 618
        }
    }
}
