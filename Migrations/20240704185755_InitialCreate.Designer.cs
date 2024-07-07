﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication3.Data;

#nullable disable

namespace WebApplication3.Migrations
{
    [DbContext(typeof(ChessDbContext))]
    [Migration("20240704185755_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("blackPlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("gameTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("gameTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("whitePlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("winnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("blackPlayerId");

                    b.HasIndex("gameTypeId");

                    b.HasIndex("whitePlayerId");

                    b.HasIndex("winnerId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameTypes");
                });

            modelBuilder.Entity("Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Game", b =>
                {
                    b.HasOne("Player", "blackPlayer")
                        .WithMany()
                        .HasForeignKey("blackPlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("GameType", "gameType")
                        .WithMany()
                        .HasForeignKey("gameTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Player", "whitePlayer")
                        .WithMany()
                        .HasForeignKey("whitePlayerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Player", "winner")
                        .WithMany()
                        .HasForeignKey("winnerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("blackPlayer");

                    b.Navigation("gameType");

                    b.Navigation("whitePlayer");

                    b.Navigation("winner");
                });
#pragma warning restore 612, 618
        }
    }
}