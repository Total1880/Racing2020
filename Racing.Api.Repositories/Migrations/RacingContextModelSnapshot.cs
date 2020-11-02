﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Racing.Api.Repositories;

namespace Racing.Api.Repositories.Migrations
{
    [DbContext(typeof(RacingContext))]
    partial class RacingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Racing.Model.FirstNames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NationId");

                    b.ToTable("FirstNamesList");
                });

            modelBuilder.Entity("Racing.Model.LastNames", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NationId");

                    b.ToTable("LastNamesList");
                });

            modelBuilder.Entity("Racing.Model.Nation", b =>
                {
                    b.Property<int>("NationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NationId");

                    b.ToTable("NationList");
                });

            modelBuilder.Entity("Racing.Model.Race", b =>
                {
                    b.Property<int>("RaceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrizeMoneyForOnePoint")
                        .HasColumnType("int");

                    b.HasKey("RaceId");

                    b.ToTable("RaceList");
                });

            modelBuilder.Entity("Racing.Model.RacePart", b =>
                {
                    b.Property<int>("RacePartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("End")
                        .HasColumnType("int");

                    b.Property<int>("Part")
                        .HasColumnType("int");

                    b.Property<int?>("RaceId")
                        .HasColumnType("int");

                    b.Property<int>("Start")
                        .HasColumnType("int");

                    b.HasKey("RacePartId");

                    b.HasIndex("RaceId");

                    b.ToTable("RacePartList");
                });

            modelBuilder.Entity("Racing.Model.RacePoint", b =>
                {
                    b.Property<int>("RacePointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int?>("RaceId")
                        .HasColumnType("int");

                    b.HasKey("RacePointId");

                    b.HasIndex("RaceId");

                    b.ToTable("RacePoint");
                });

            modelBuilder.Entity("Racing.Model.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SettingList");
                });

            modelBuilder.Entity("Racing.Model.Team", b =>
                {
                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamId");

                    b.ToTable("TeamList");
                });

            modelBuilder.Entity("Racing.Model.FirstNames", b =>
                {
                    b.HasOne("Racing.Model.Nation", "Nation")
                        .WithMany()
                        .HasForeignKey("NationId");
                });

            modelBuilder.Entity("Racing.Model.LastNames", b =>
                {
                    b.HasOne("Racing.Model.Nation", "Nation")
                        .WithMany()
                        .HasForeignKey("NationId");
                });

            modelBuilder.Entity("Racing.Model.RacePart", b =>
                {
                    b.HasOne("Racing.Model.Race", "Race")
                        .WithMany("RacePartList")
                        .HasForeignKey("RaceId");
                });

            modelBuilder.Entity("Racing.Model.RacePoint", b =>
                {
                    b.HasOne("Racing.Model.Race", "Race")
                        .WithMany("RacePointList")
                        .HasForeignKey("RaceId");
                });
#pragma warning restore 612, 618
        }
    }
}
