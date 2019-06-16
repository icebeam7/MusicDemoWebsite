﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MusicDemoWebsite.Context;

namespace MusicDemoWebsite.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MusicDemoWebsite.Models.Artist", b =>
                {
                    b.Property<Guid>("ArtistID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArtistName");

                    b.HasKey("ArtistID");

                    b.ToTable("ArtistTable");
                });

            modelBuilder.Entity("MusicDemoWebsite.Models.Song", b =>
                {
                    b.Property<Guid>("SongID")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ArtistID");

                    b.Property<string>("SongName");

                    b.HasKey("SongID");

                    b.HasIndex("ArtistID");

                    b.ToTable("SongTable");
                });

            modelBuilder.Entity("MusicDemoWebsite.Models.Song", b =>
                {
                    b.HasOne("MusicDemoWebsite.Models.Artist", "Artist")
                        .WithMany("Songs")
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
