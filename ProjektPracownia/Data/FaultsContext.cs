using Microsoft.EntityFrameworkCore;
using ProjektPracownia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektPracownia.Data

    
{
    public interface IDbContext
    {
        DbSet<CarMake> CarMakes { get; set; }
        DbSet<CarModel> CarModels { get; set; }
        //...other properties and members needed for db context
        int SaveChanges();
    }

    public class FaultsContext : DbContext, IDbContext
    {
        public FaultsContext(DbContextOptions<FaultsContext> options) : base(options)
        {
        }

        public virtual DbSet<CarMake> CarMakes { get; set; }
        public virtual DbSet<CarModel> CarModels { get; set; }
        public virtual DbSet<CarVersion> CarVersion { get; set; }
        public virtual DbSet<CarFault> CarFaults { get; set; }
        public virtual DbSet<FaultConnection> FaultConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarMake>().ToTable("CarMake");
            modelBuilder.Entity<CarModel>().ToTable("CarModel");
            modelBuilder.Entity<CarVersion>().ToTable("CarVersion");
            modelBuilder.Entity<CarFault>().ToTable("CarFault");
            modelBuilder.Entity<FaultConnection>().ToTable("FaultConnection");
        }
    }
}