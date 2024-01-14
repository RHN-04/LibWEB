using System;
using System.Collections.Generic;
using LibWEB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibWEB.Data
{
    public partial class LibContext : DbContext
    {
        public LibContext()
        {
        }

        public LibContext(DbContextOptions<LibContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<AuthorPrintPublishing> AuthorPrintPublishings { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<GenrePrintPublishing> GenrePrintPublishings { get; set; } = null!;
        public virtual DbSet<Giving> Givings { get; set; } = null!;
        public virtual DbSet<Preorder> Preorders { get; set; } = null!;
        public virtual DbSet<PrintPublishing> PrintPublishings { get; set; } = null!;
        public virtual DbSet<Reader> Readers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user=root;password=R4h4-ch4n_o@;database=library_web", ServerVersion.Parse("8.0.32-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.HasIndex(e => e.Country, "country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country).HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(255)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .HasColumnName("surname");

                entity.HasOne(d => d.CountryNavigation)
                    .WithMany(p => p.Authors)
                    .HasForeignKey(d => d.Country)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_ibfk_1");
            });

            modelBuilder.Entity<AuthorPrintPublishing>(entity =>
            {
                entity.HasKey(ap => new { ap.Author, ap.PrintPublishing });

                entity.ToTable("author_print_publishing");

                entity.HasIndex(e => e.Author, "author");

                entity.HasIndex(e => e.PrintPublishing, "print_publishing");

                entity.Property(e => e.Author).HasColumnName("author");

                entity.Property(e => e.PrintPublishing).HasColumnName("print_publishing");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("author_print_publishing_ibfk_1");

                entity.HasOne(d => d.PrintPublishingNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PrintPublishing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("author_print_publishing_ibfk_2");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("country_name");
            });

            modelBuilder.Entity<Efmigrationshistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__efmigrationshistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion).HasMaxLength(32);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NameGenre)
                    .HasMaxLength(50)
                    .HasColumnName("name_genre");
            });

            modelBuilder.Entity<GenrePrintPublishing>(entity =>
            {
                entity.HasKey(gp => new { gp.Genre, gp.PrintPublishing });

                entity.ToTable("genre_print_publishing");

                entity.HasIndex(e => e.Genre, "genre");

                entity.HasIndex(e => e.PrintPublishing, "print_publishing");

                entity.Property(e => e.Genre).HasColumnName("genre");

                entity.Property(e => e.PrintPublishing).HasColumnName("print_publishing");

                entity.HasOne(d => d.GenreNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Genre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("genre_print_publishing_ibfk_1");

                entity.HasOne(d => d.PrintPublishingNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PrintPublishing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("genre_print_publishing_ibfk_2");
            });


            modelBuilder.Entity<Giving>(entity =>
            {
                entity.ToTable("giving");

                entity.HasIndex(e => e.Reader, "reader");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GivingDate).HasColumnName("giving_date");

                entity.Property(e => e.Reader).HasColumnName("reader");

                entity.Property(e => e.ReceivingDate).HasColumnName("receiving_date");

                entity.Property(e => e.ReceivingDeadlineDate).HasColumnName("receiving_deadline_date");

                entity.Property(e => e.PrintPublishing).HasColumnName("print_publishing");

                entity.HasOne(d => d.ReaderNavigation)
                    .WithMany(p => p.Givings)
                    .HasForeignKey(d => d.Reader)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("giving_ibfk_1");

                entity.HasOne(d => d.PrintPublishingNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PrintPublishing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_print_publishing");
            });

            modelBuilder.Entity<Preorder>(entity =>
            {
                entity.ToTable("preorder");

                entity.HasIndex(e => e.Reader, "reader");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GivingDeadlineDate).HasColumnName("giving_deadline_date");

                entity.Property(e => e.Reader).HasColumnName("reader");

                entity.Property(e => e.Status)
                    .HasColumnType("enum('created','done','archived')")
                    .HasColumnName("status");

                entity.Property(e => e.PrintPublishing).HasColumnName("print_publishing");

                entity.HasOne(d => d.ReaderNavigation)
                    .WithMany(p => p.Preorders)
                    .HasForeignKey(d => d.Reader)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("preorder_ibfk_1");

                entity.HasOne(d => d.PrintPublishingNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.PrintPublishing)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_print_publishing_1");
            });

            modelBuilder.Entity<PrintPublishing>(entity =>
            {
                entity.ToTable("print_publishing");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AgeRestriction).HasColumnName("age_restriction");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.ImageId)
                    .HasMaxLength(225)
                    .HasColumnName("image_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Numbers).HasColumnName("numbers");

                entity.Property(e => e.YearOfPublishing).HasColumnName("year_of_publishing");
            });

            modelBuilder.Entity<Reader>(entity =>
            {
                entity.ToTable("reader");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(255)
                    .HasColumnName("email_address");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Patronymic)
                    .HasMaxLength(255)
                    .HasColumnName("patronymic");

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
