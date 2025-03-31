using System;
using System;

namespace Application.Domain.Entities
{
    public class ServiceAppointmentService
    {
        public int ServiceAppointmentId { get; set; }
        public ServiceAppointment ServiceAppointment { get; set; } = null!;

        public int ServiceOfferedId { get; set; }
        public ServiceOffered ServiceOffered { get; set; } = null!;
    }
}
