using System;
namespace Application.Domain.Entities
{
    public class ServiceCenter
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty; // Service center name
        public string PinCode { get; set; } = string.Empty; // Pin code for search
        public string Address { get; set; } = string.Empty; // Full address
        public string PhoneNumber { get; set; } = string.Empty; // Contact info
        public List<string> BrandsSupported { get; set; } = new(); // List of supported brands

        // Relationship with Service Appointments
        public List<ServiceAppointment> ServiceAppointments { get; set; } = new();

        public List<ServiceOffered> ServicesOffered { get; set; } = new();
    }
}