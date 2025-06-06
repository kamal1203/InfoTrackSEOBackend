﻿// <auto-generated />
using System;
using InfoTrackSEO.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InfoTrackSEO.Data.Migrations
{
    [DbContext(typeof(SeoDbContext))]
    [Migration("20250424190714_SeedSampleSearchResults")]
    partial class SeedSampleSearchResults
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InfoTrackSEO.Core.Models.SearchResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Keywords")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RankPositions")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScrapingStrategy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SearchEngine")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SearchResults");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            Keywords = "seo tools",
                            RankPositions = "1,3,7",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 23, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4073),
                            Url = "example.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111112"),
                            Keywords = "best laptops",
                            RankPositions = "2,5",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 22, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4420),
                            Url = "laptopworld.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111113"),
                            Keywords = "c# tutorials",
                            RankPositions = "1",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 21, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4424),
                            Url = "dotnetdocs.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111114"),
                            Keywords = "web scraping",
                            RankPositions = "4,8",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 20, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4426),
                            Url = "scrapemaster.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111115"),
                            Keywords = "ai news",
                            RankPositions = "1,2",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 19, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4427),
                            Url = "ainews.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111116"),
                            Keywords = "football scores",
                            RankPositions = "10",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 18, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4429),
                            Url = "sportszone.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111117"),
                            Keywords = "weather today",
                            RankPositions = "1",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 17, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4430),
                            Url = "weather.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111118"),
                            Keywords = "stock market",
                            RankPositions = "3,6",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 16, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4432),
                            Url = "financehub.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111119"),
                            Keywords = "dotnet core",
                            RankPositions = "1,2,5",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 15, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4433),
                            Url = "microsoft.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111120"),
                            Keywords = "travel deals",
                            RankPositions = "7,8",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 14, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4434),
                            Url = "travelnow.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111121"),
                            Keywords = "healthy recipes",
                            RankPositions = "2,4",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 13, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4436),
                            Url = "foodie.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111122"),
                            Keywords = "music charts",
                            RankPositions = "1,3",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 12, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4437),
                            Url = "musiczone.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111123"),
                            Keywords = "movie reviews",
                            RankPositions = "5,9",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 11, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4439),
                            Url = "cinemaworld.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111124"),
                            Keywords = "coding bootcamp",
                            RankPositions = "1,2",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 10, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4442),
                            Url = "codecamp.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111125"),
                            Keywords = "fitness tips",
                            RankPositions = "3,4",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 9, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4443),
                            Url = "fitlife.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111126"),
                            Keywords = "gardening guide",
                            RankPositions = "1,5",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 8, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4445),
                            Url = "gardeners.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111127"),
                            Keywords = "startup news",
                            RankPositions = "2,6",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 7, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4446),
                            Url = "startupdaily.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111128"),
                            Keywords = "python libraries",
                            RankPositions = "1,3,8",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 6, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4448),
                            Url = "pythontools.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111129"),
                            Keywords = "car reviews",
                            RankPositions = "4,7",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 5, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4449),
                            Url = "carzone.com"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111130"),
                            Keywords = "fashion trends",
                            RankPositions = "2,5",
                            ScrapingStrategy = "PowerShell",
                            SearchEngine = "Google",
                            Timestamp = new DateTime(2025, 4, 4, 19, 7, 13, 904, DateTimeKind.Utc).AddTicks(4451),
                            Url = "fashionista.com"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
