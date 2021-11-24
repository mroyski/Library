using System;
using Library.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Library.Api.Models
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
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Library;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.Author)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("author");

                entity.Property(e => e.BookName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("book_name");

                entity.Property(e => e.Category)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Cost).HasColumnName("cost");
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.HasKey(e => e.LibIssueId)
                    .HasName("PK__issue__3927D4DBE54BDB27");

                entity.ToTable("issue");

                entity.Property(e => e.LibIssueId).HasColumnName("lib_issue_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("date")
                    .HasColumnName("issue_date");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("date")
                    .HasColumnName("return_date");
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("member");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.AccOpenDate)
                    .HasColumnType("date")
                    .HasColumnName("acc_open_date");

                entity.Property(e => e.MaxBooksAllowed).HasColumnName("max_books_allowed");

                entity.Property(e => e.MemberName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("member_name");

                entity.Property(e => e.PenaltyAmount).HasColumnName("penalty_amount");
            });

            modelBuilder.Entity<Reserve>(entity =>
            {
                entity.ToTable("reserve");

                entity.Property(e => e.ReserveId).HasColumnName("reserve_id");

                entity.Property(e => e.BookId).HasColumnName("book_id");

                entity.Property(e => e.MemberId).HasColumnName("member_id");

                entity.Property(e => e.ReserveDate)
                    .HasColumnType("date")
                    .HasColumnName("reserve_date");

                entity.Property(e => e.ReserveStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("reserve_status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
