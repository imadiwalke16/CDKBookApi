using System;
namespace Application.DTOs.ServiceAppointment
{
    public class ServiceAppointmentServiceDTO
    {
        public int ServiceOfferedId { get; set; }
        public string ServiceName { get; set; } = string.Empty; // Add service name for clarity
    }
}
