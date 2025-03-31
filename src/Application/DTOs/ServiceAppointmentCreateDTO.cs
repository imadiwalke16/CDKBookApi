using System;
using System;
using System.Collections.Generic;

namespace Application.DTOs.ServiceAppointment
{
    public class ServiceAppointmentCreateDTO
    {
        public DateTime AppointmentDate { get; set; }
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public int ServiceCenterId { get; set; }
        public string TransportMode { get; set; } = string.Empty;
        public decimal TransportCharge { get; set; }
        public List<int> ServiceOfferedIds { get; set; } = new(); // ✅ List of selected services
    }
}

