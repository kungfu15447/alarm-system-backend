﻿// <auto-generated />
using System;
using AlarmSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace infrastructure.Migrations
{
    [DbContext(typeof(SystemContext))]
    partial class SystemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.Alarm", b =>
                {
                    b.Property<int>("AlarmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AlarmId");

                    b.ToTable("Alarms");
                });

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.AlarmLog", b =>
                {
                    b.Property<int>("AlarmId")
                        .HasColumnType("int");

                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("Date")
                        .HasColumnType("bigint");

                    b.HasKey("AlarmId", "MachineId", "Date");

                    b.HasIndex("MachineId");

                    b.ToTable("AlarmLogs");
                });

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.AlarmWatch", b =>
                {
                    b.Property<int>("AlarmId")
                        .HasColumnType("int");

                    b.Property<string>("WatchId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("AlarmId", "WatchId");

                    b.ToTable("AlarmWatch");
                });

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.Machine", b =>
                {
                    b.Property<string>("MachineId")
                        .HasColumnName("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.MachineWatch", b =>
                {
                    b.Property<string>("MachineId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("WatchId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MachineId", "WatchId");

                    b.ToTable("MachineWatch");
                });

            modelBuilder.Entity("Core.Entity.DB.User", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.AlarmLog", b =>
                {
                    b.HasOne("AlarmSystem.Core.Entity.DB.Alarm", "Alarm")
                        .WithMany()
                        .HasForeignKey("AlarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AlarmSystem.Core.Entity.DB.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.AlarmWatch", b =>
                {
                    b.HasOne("AlarmSystem.Core.Entity.DB.Alarm", "Alarm")
                        .WithMany()
                        .HasForeignKey("AlarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AlarmSystem.Core.Entity.DB.MachineWatch", b =>
                {
                    b.HasOne("AlarmSystem.Core.Entity.DB.Machine", "Machine")
                        .WithMany()
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
