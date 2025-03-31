using System;
namespace Application.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
        public string imgUrl { get; set; } = string.Empty;

        // ✅ Many Vehicles -> One User
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // ✅ One Vehicle -> Many Service Appointments
        public List<ServiceAppointment> ServiceAppointments { get; set; } = new();

        // ✅ One Vehicle -> Many Service Histories
        public List<ServiceHistory> ServiceHistories { get; set; } = new();
    }
}
