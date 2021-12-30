﻿// <auto-generated />
using System;
using Library.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Library.Api.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Library.Shared.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Cost")
                        .HasColumnType("float");

                    b.HasKey("BookId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "Mike Royski",
                            BookName = "Hello World!",
                            Category = "Technology",
                            Cost = 10.0
                        });
                });

            modelBuilder.Entity("Library.Shared.Issue", b =>
                {
                    b.Property<int>("LibIssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("IssueDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.HasKey("LibIssueId");

                    b.ToTable("Issues");

                    b.HasData(
                        new
                        {
                            LibIssueId = 1,
                            BookId = 1,
                            IssueDate = new DateTime(2021, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            MemberId = 1
                        });
                });

            modelBuilder.Entity("Library.Shared.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AccOpenDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MaxBooksAllowed")
                        .HasColumnType("int");

                    b.Property<string>("MemberName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PenaltyAmount")
                        .HasColumnType("int");

                    b.HasKey("MemberId");

                    b.ToTable("Members");

                    b.HasData(
                        new
                        {
                            MemberId = 1,
                            AccOpenDate = new DateTime(2021, 12, 30, 18, 3, 49, 444, DateTimeKind.Local).AddTicks(1993),
                            MaxBooksAllowed = 5,
                            MemberName = "Cam Schaefer",
                            PenaltyAmount = 0
                        });
                });

            modelBuilder.Entity("Library.Shared.Reserve", b =>
                {
                    b.Property<int>("ReserveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookId")
                        .HasColumnType("int");

                    b.Property<int?>("MemberId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReserveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReserveStatus")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReserveId");

                    b.ToTable("Reserves");

                    b.HasData(
                        new
                        {
                            ReserveId = 1,
                            BookId = 1,
                            MemberId = 1,
                            ReserveDate = new DateTime(2021, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReserveStatus = "pending"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
