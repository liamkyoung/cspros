﻿// <auto-generated />
using System;
using CS2DemoAnalyzer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CS2DemoAnalyzer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240704142623_RemovedSkinUsageFromPlayer")]
    partial class RemovedSkinUsageFromPlayer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AbbreviatedName")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.Property<string>("CountryImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GameMapId")
                        .HasColumnType("integer");

                    b.Property<string>("HltvLink")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("HltvStars")
                        .HasColumnType("integer");

                    b.Property<bool>("MatchParsed")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("StartedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TeamAId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamAScore")
                        .HasColumnType("integer");

                    b.Property<int>("TeamBId")
                        .HasColumnType("integer");

                    b.Property<int>("TeamBScore")
                        .HasColumnType("integer");

                    b.Property<int>("WinnerId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GameMapId");

                    b.HasIndex("TeamAId");

                    b.HasIndex("TeamBId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DemoName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GamesPlayedOnMap")
                        .HasColumnType("integer");

                    b.Property<string>("MapLayoutImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfileImage")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<int>("Dislikes")
                        .HasColumnType("integer");

                    b.Property<string>("GamerTag")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("HltvLink")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("character varying(75)");

                    b.Property<string[]>("Nicknames")
                        .HasColumnType("text[]");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("text");

                    b.Property<int?>("TeamId")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset>("TimeSinceLastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("GamerTag")
                        .IsUnique();

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.PlayerSocials", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<string>("Instagram")
                        .HasColumnType("text");

                    b.Property<string>("Twitch")
                        .HasColumnType("text");

                    b.Property<string>("Twitter")
                        .HasColumnType("text");

                    b.HasKey("PlayerId");

                    b.ToTable("PlayerSocials");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Skin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Dislikes")
                        .HasColumnType("integer");

                    b.Property<string>("ImgSrc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Likes")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rarity")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WeaponId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("WeaponId", "Name")
                        .IsUnique();

                    b.ToTable("Skins");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.SkinUsage", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("integer");

                    b.Property<int>("GameId")
                        .HasColumnType("integer");

                    b.Property<int>("SkinId")
                        .HasColumnType("integer");

                    b.Property<int>("KillsInGame")
                        .HasColumnType("integer");

                    b.HasKey("PlayerId", "GameId", "SkinId");

                    b.HasIndex("GameId");

                    b.HasIndex("SkinId");

                    b.ToTable("SkinUsages");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CoachName")
                        .HasColumnType("text");

                    b.Property<int?>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("HltvProfile")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("HltvRanking")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("TimeSinceLastUpdated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DemoName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TeamAssigned")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.WeaponItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int?>("SkinId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SkinId");

                    b.ToTable("WeaponItems");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Game", b =>
                {
                    b.HasOne("CS2DemoAnalyzer.Models.Map", "GameMap")
                        .WithMany()
                        .HasForeignKey("GameMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CS2DemoAnalyzer.Models.Team", "TeamA")
                        .WithMany()
                        .HasForeignKey("TeamAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CS2DemoAnalyzer.Models.Team", "TeamB")
                        .WithMany()
                        .HasForeignKey("TeamBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CS2DemoAnalyzer.Models.Team", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameMap");

                    b.Navigation("TeamA");

                    b.Navigation("TeamB");

                    b.Navigation("Winner");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Player", b =>
                {
                    b.HasOne("CS2DemoAnalyzer.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("CS2DemoAnalyzer.Models.Team", "Team")
                        .WithMany("Players")
                        .HasForeignKey("TeamId");

                    b.Navigation("Country");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.PlayerSocials", b =>
                {
                    b.HasOne("CS2DemoAnalyzer.Models.Player", null)
                        .WithOne("Socials")
                        .HasForeignKey("CS2DemoAnalyzer.Models.PlayerSocials", "PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Skin", b =>
                {
                    b.HasOne("CS2DemoAnalyzer.Models.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.SkinUsage", b =>
                {
                    b.HasOne("CS2DemoAnalyzer.Models.Game", null)
                        .WithMany("SkinUsages")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CS2DemoAnalyzer.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CS2DemoAnalyzer.Models.Skin", null)
                        .WithMany()
                        .HasForeignKey("SkinId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Team", b =>
                {
                    b.HasOne("CS2DemoAnalyzer.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.WeaponItem", b =>
                {
                    b.HasOne("CS2DemoAnalyzer.Models.Skin", "Skin")
                        .WithMany()
                        .HasForeignKey("SkinId");

                    b.Navigation("Skin");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Game", b =>
                {
                    b.Navigation("SkinUsages");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Player", b =>
                {
                    b.Navigation("Socials");
                });

            modelBuilder.Entity("CS2DemoAnalyzer.Models.Team", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
