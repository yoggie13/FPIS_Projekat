using FPIS_Projekat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FPIS_Projekat.Data
{
    public class ISContext : DbContext
    {
        private ISContext(DbContextOptions
       <ISContext> options) : base(options)
        {

        }
        private static ISContext _context;
        public static ISContext getContext()
        {
            if (_context == null)
                _context = new ISContext();
            return _context;
        }
        public ISContext() { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<TariffPackage> TariffPackages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Database = DatabaseFPIS; Trusted_Connection = True; " +
                "MultipleActiveResultSets = true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<OfferItem>()
                .HasOne(o => o._Offer)
                .WithMany(of => of.OfferItems);

            modelBuilder.Entity<OfferItem>()
                .HasOne(o => o._TariffPackage)
                .WithMany(t => t.Offers);

            modelBuilder.Entity<OfferItem>()
                .HasOne(o => o._Device)
                .WithMany(d => d.Offers);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o._Client)
                .WithMany(c => c.OffersReceived);

            modelBuilder.Entity<Offer>()
               .HasOne(o => o._Employee)
               .WithMany(e => e.OffersMade);

            modelBuilder.Entity<Device>()
                 .HasOne(d => d._Manufacturer)
                 .WithMany(m => m.Devices);

            modelBuilder.Entity<TariffPackage>()
                .HasOne(t => t._PackageType)
                .WithMany(p => p.TariffPackages);


        }
    }
}
