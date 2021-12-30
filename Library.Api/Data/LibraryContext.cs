using System;
using Library.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Library.Api.Data
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Issue> Issues { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Reserve> Reserves { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Library;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book()
                {
                    BookId = 1,
                    BookName = "Hello World!",
                    Author = "Mike Royski",
                    Category = "Technology",
                    Cost = 10.00
                });

            modelBuilder.Entity<Member>().HasData(
                new Member()
                {
                    MemberId = 1,
                    MemberName = "Cam Schaefer",
                    MaxBooksAllowed = 5,
                    PenaltyAmount = 0,
                    AccOpenDate = DateTime.Now
                });

            modelBuilder.Entity<Reserve>().HasData(
                new Reserve()
                {
                    ReserveId = 1,
                    BookId = 1,
                    MemberId = 1,
                    ReserveDate = DateTime.Parse("12/20/2021"),
                    ReserveStatus = "pending"
                });

            modelBuilder.Entity<Issue>().HasData(
                new Issue()
                {
                    LibIssueId = 1,
                    BookId = 1,
                    MemberId = 1,
                    IssueDate = DateTime.Parse("12/21/2021"),
                    ReturnDate = null
                });
        }
    }
}
