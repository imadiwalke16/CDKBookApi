using Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // ✅ Added Missing Tables
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceAppointment> ServiceAppointments { get; set; }
        public DbSet<ServiceHistory> ServiceHistories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ServiceCenter> ServiceCenters { get; set; }
        public DbSet<ServiceOffered> ServicesOffered { get; set; } // 🔹 Added ServiceOffered Table
        public DbSet<ServiceAppointmentService> ServiceAppointmentServices { get; set; } // 🔹 Many-to-Many Table


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ✅ Enforce Unique Email for Users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // ✅ One-to-Many: User -> Vehicles
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithMany(u => u.Vehicles)
                .HasForeignKey(v => v.UserId);

            // ✅ One-to-Many: User -> Service Appointments
            modelBuilder.Entity<ServiceAppointment>()
                .HasOne(sa => sa.User)
                .WithMany(u => u.ServiceAppointments)
                .HasForeignKey(sa => sa.UserId);

            // ✅ One-to-Many: Vehicle -> Service Appointments
            modelBuilder.Entity<ServiceAppointment>()
                .HasOne(sa => sa.Vehicle)
                .WithMany(v => v.ServiceAppointments)
                .HasForeignKey(sa => sa.VehicleId);

            // ✅ One-to-Many: Vehicle -> Service Histories
            modelBuilder.Entity<ServiceHistory>()
                .HasOne(sh => sh.Vehicle)
                .WithMany(v => v.ServiceHistories)
                .HasForeignKey(sh => sh.VehicleId);

            // ✅ One-to-Many: User -> Notifications
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId);

            // ✅ One-to-Many: ServiceCenter -> ServiceOffered
            modelBuilder.Entity<ServiceOffered>()
                .HasOne(so => so.ServiceCenter)
                .WithMany(sc => sc.ServicesOffered)
                .HasForeignKey(so => so.ServiceCenterId);

            // ✅ One-to-Many: ServiceCenter -> ServiceAppointments
            modelBuilder.Entity<ServiceAppointment>()
                .HasOne(sa => sa.ServiceCenter)
                .WithMany(sc => sc.ServiceAppointments)
                .HasForeignKey(sa => sa.ServiceCenterId);

            // ✅ Many-to-Many: ServiceAppointment ↔ ServiceOffered
            modelBuilder.Entity<ServiceAppointmentService>()
                .HasKey(sas => new { sas.ServiceAppointmentId, sas.ServiceOfferedId }); // Composite Primary Key

            modelBuilder.Entity<ServiceAppointmentService>()
                .HasOne(sas => sas.ServiceAppointment)
                .WithMany(sa => sa.ServiceAppointmentServices)
                .HasForeignKey(sas => sas.ServiceAppointmentId);

            modelBuilder.Entity<ServiceAppointmentService>()
                .HasOne(sas => sas.ServiceOffered)
                .WithMany(so => so.ServiceAppointmentServices)
                .HasForeignKey(sas => sas.ServiceOfferedId);
        }
    }
}
