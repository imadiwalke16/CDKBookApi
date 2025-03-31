using System;
using System;
using System.Collections.Generic;
using Application.DTOs.User;
using Application.DTOs.Vehicle;
using Application.DTOs.ServiceCenter;

namespace Application.DTOs.ServiceAppointment
{
    public class ServiceAppointmentDTO
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public int UserId { get; set; }
        public UserDTO? User { get; set; }
        public int VehicleId { get; set; }
        public VehicleDTO? Vehicle { get; set; }
        public int ServiceCenterId { get; set; }
        public ServiceCenterDTO? ServiceCenter { get; set; }
        public string TransportMode { get; set; } = string.Empty;
        public decimal TransportCharge { get; set; }
        public List<ServiceAppointmentServiceDTO>? ServiceAppointmentServices { get; set; }
    }
}
