using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Assignment.Models
{
    public partial class AssignmentContext : DbContext
    {
        public AssignmentContext()
        {
        }

        public AssignmentContext(DbContextOptions<AssignmentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Collection> Collections { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Assignment;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.ToTable("Collection");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collection_Song");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collections)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Collection_Users");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Song)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.SongId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Song");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Users");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Song_Artist");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
