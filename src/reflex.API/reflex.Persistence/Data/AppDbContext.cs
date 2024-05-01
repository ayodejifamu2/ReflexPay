using Microsoft.EntityFrameworkCore;
using reflex.Domain;
using reflex.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Persistence.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers{ get; set; }
        public DbSet<Otp> Otps { get; set; }
        public DbSet<CardInfo> Cards { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.id);
                entity.Property(c => c.id).ValueGeneratedOnAdd(); // Configure Id as auto-increment
                entity.Property(c => c.customerPhoneNumber).HasMaxLength(20).IsRequired();
                entity.Property(c => c.firstName).HasMaxLength(100);
                entity.Property(c => c.lastName).HasMaxLength(100);
                entity.Property(c => c.userName).HasMaxLength(50);

                // Create indexes on Id and PhoneNumber
                entity.HasIndex(c => c.id).IsUnique();
                entity.HasIndex(c => c.customerPhoneNumber).IsUnique();
            });

            modelBuilder.Entity<Otp>(entity =>
            {
                entity.HasKey(r => r.id);
                entity.Property(r => r.id).ValueGeneratedOnAdd(); // Configure Id as auto-increment
            });

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
