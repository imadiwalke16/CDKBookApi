using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ✅ Added Missing Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Dealer> Dealers { get; set; } // Added
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceAppointment> ServiceAppointments { get; set; }
        public DbSet<ServiceHistory> ServiceHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ServiceCenter> ServiceCenters { get; set; }
        public DbSet<ServiceOffered> ServicesOffered { get; set; } // 🔹 Added ServiceOffered Table
        public DbSet<ServiceAppointmentService> ServiceAppointmentServices { get; set; } // 🔹 Many-to-Many Table


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing configurations (keep unchanged)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId);
            modelBuilder.Entity<ServiceAppointment>()
                .HasOne(sa => sa.User)
                .WithMany(u => u.ServiceAppointments)
                .HasForeignKey(sa => sa.UserId);
            modelBuilder.Entity<ServiceAppointment>()
                .HasOne(sa => sa.Vehicle)
                .WithMany(v => v.ServiceAppointments)
                .HasForeignKey(sa => sa.VehicleId);
            modelBuilder.Entity<ServiceHistory>()
                .HasOne(sh => sh.Vehicle)
                .WithMany(v => v.ServiceHistories)
                .HasForeignKey(sh => sh.VehicleId);
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);
            modelBuilder.Entity<ServiceOffered>()
                .HasOne(so => so.ServiceCenter)
                .WithMany(sc => sc.ServicesOffered)
                .HasForeignKey(so => so.ServiceCenterId);
            modelBuilder.Entity<ServiceAppointment>()
                .HasOne(sa => sa.ServiceCenter)
                .WithMany(sc => sc.ServiceAppointments)
                .HasForeignKey(sa => sa.ServiceCenterId);
            modelBuilder.Entity<ServiceAppointmentService>()
                .HasKey(sas => new { sas.ServiceAppointmentId, sas.ServiceOfferedId });
            modelBuilder.Entity<ServiceAppointmentService>()
                .HasOne(sas => sas.ServiceAppointment)
                .WithMany(sa => sa.ServiceAppointmentServices)
                .HasForeignKey(sas => sas.ServiceAppointmentId);
            modelBuilder.Entity<ServiceAppointmentService>()
                .HasOne(sas => sas.ServiceOffered)
                .WithMany(so => so.ServiceAppointmentServices)
                .HasForeignKey(sas => sas.ServiceOfferedId);

            // Seed Dealers
            modelBuilder.Entity<Dealer>().HasData(
    new Dealer
    {
        DealerId = 36001,
        Cid = "320004",
        Name = "Ford",
        StaticOtp = "123456",
        ThemeConfig = "{\"navbarColor\": \"#0000FF\"}",
        Code = "ABC123"
    },
    new Dealer
    {
        DealerId = 36002,
        Cid = "330005",
        Name = "Chevy",
        StaticOtp = "123456",
        ThemeConfig = "{\"navbarColor\": \"#FF0000\"}",
        Code = "XYZ789"
    }
);

            // Seed User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Akanksha Agrawal",
                    Email = "anuradha.yele@cdk.com",
                    PasswordHash = "$2a$12$t8DhVKV9fZpdrXtYhWhKduZPIDsHySlUSzgq38dHgOO4wUxIbfDri",
                    Role = "Customer",
                    PhoneNumber = "(815) 982-7993",
                    CustomerId = "2047e667-22ad-49fa-bed4-9337eefb4023",
                    DealerId = 36001 // Corrected to match Ford's DealerId
                }
            );

            // Unique constraint on Cid
            modelBuilder.Entity<Dealer>()
                .HasIndex(d => d.Cid)
                .IsUnique();
        }
    }
}
