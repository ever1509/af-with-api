﻿// <auto-generated />
using System;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Click", b =>
                {
                    b.Property<int>("ClickId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClickId"), 1L, 1);

                    b.Property<string>("Browser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDateClick")
                        .HasColumnType("datetime2");

                    b.Property<string>("Platform")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UrlId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClickId");

                    b.HasIndex("UrlId");

                    b.ToTable("Clicks");
                });

            modelBuilder.Entity("Domain.Entities.Url", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDateUrl")
                        .HasColumnType("datetime2");

                    b.Property<string>("OriginalUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Urls");
                });

            modelBuilder.Entity("Domain.Entities.Click", b =>
                {
                    b.HasOne("Domain.Entities.Url", "Url")
                        .WithMany("Clicks")
                        .HasForeignKey("UrlId")
                        .IsRequired()
                        .HasConstraintName("FK_Url_Click");

                    b.Navigation("Url");
                });

            modelBuilder.Entity("Domain.Entities.Url", b =>
                {
                    b.Navigation("Clicks");
                });
#pragma warning restore 612, 618
        }
    }
}