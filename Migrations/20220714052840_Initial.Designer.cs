﻿// <auto-generated />
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment.Migrations
{
    [DbContext(typeof(AssignmentContext))]
    [Migration("20220714052840_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Assignment.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Artist", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.Collection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int")
                        .HasColumnName("ArtistID");

                    b.Property<int>("SongId")
                        .HasColumnType("int")
                        .HasColumnName("SongID");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.HasIndex("SongId");

                    b.ToTable("Collection", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ArtistId")
                        .HasColumnType("int")
                        .HasColumnName("ArtistID");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Song", (string)null);
                });

            modelBuilder.Entity("Assignment.Models.Collection", b =>
                {
                    b.HasOne("Assignment.Models.Artist", "Artist")
                        .WithMany("Collections")
                        .HasForeignKey("ArtistId")
                        .IsRequired()
                        .HasConstraintName("FK_Collection_Artist");

                    b.HasOne("Assignment.Models.Song", "Song")
                        .WithMany("Collections")
                        .HasForeignKey("SongId")
                        .IsRequired()
                        .HasConstraintName("FK_Collection_Song");

                    b.Navigation("Artist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Assignment.Models.Song", b =>
                {
                    b.HasOne("Assignment.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .IsRequired()
                        .HasConstraintName("FK_Song_Artist");

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("Assignment.Models.Artist", b =>
                {
                    b.Navigation("Collections");

                    b.Navigation("Songs");
                });

            modelBuilder.Entity("Assignment.Models.Song", b =>
                {
                    b.Navigation("Collections");
                });
#pragma warning restore 612, 618
        }
    }
}
