﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiDatabase.Entities;
using WebApiDatabase.Interfaces;

namespace WebApiDatabase
{
    public partial class AutoparkDBContext : DbContext, IAutoparkDBContext
    {
        public AutoparkDBContext()
        {
        }

        public AutoparkDBContext(DbContextOptions<AutoparkDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Car> Cars { get; set; } = null!;
        public virtual DbSet<CarType> CarTypes { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<Journal> Journals { get; set; } = null!;
        public virtual DbSet<ParkingPlace> ParkingPlaces { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>(entity =>
            {
                entity.HasIndex(e => e.RegNumber, "UQ__Cars__5D9A6740DCED2DC8")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.RegNumber)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.CarTypeNavigation)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.CarType)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Cars_CarTypes"); 

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Cars)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_Cars_Clients");
            });

            modelBuilder.Entity<CarType>(entity =>
            {
                entity.HasIndex(e => e.TypeName, "IX_CarTypes")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.TypeName).HasMaxLength(10);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => new { e.Name, e.Surname }, "UQ__Clients__2D535FA47897D182")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(50);

            });

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.ToTable("Journal");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.CarId).HasColumnName("CarID");

                entity.Property(e => e.ComingDate).HasColumnType("datetime");

                entity.Property(e => e.DepartureDate).HasColumnType("datetime");


                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Journals)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Journal_Cars");

                entity.HasOne(d => d.ParkingPlaceNavigation)
                    .WithMany(p => p.Journals)
                    .HasForeignKey(d => d.ParkingPlace)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasConstraintName("FK_Journal_ParkingPlaces");
            });

            modelBuilder.Entity<ParkingPlace>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();


                entity.HasOne(d => d.CarTypeNavigation)
                    .WithMany(p => p.ParkingPlaces)
                    .HasForeignKey(d => d.CarType)
                    .OnDelete(DeleteBehavior.NoAction)
                    .HasConstraintName("FK_ParkingPlaces_CarTypes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
