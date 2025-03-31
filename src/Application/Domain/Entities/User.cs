using System;
using System.Collections.Generic;
using MediatR;

namespace Application.Domain.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "Customer";
        public string PhoneNumber { get; set; } = string.Empty;

        // ✅ One User -> Many Vehicles
        public List<Vehicle> Vehicles { get; set; } = new();

        // ✅ One User -> Many Service Appointments
        public List<ServiceAppointment> ServiceAppointments { get; set; } = new();

        // ✅ One User -> Many Notifications
        public List<Notification> Notifications { get; set; } = new();
    }
}
