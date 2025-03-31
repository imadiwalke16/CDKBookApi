using System;

namespace Application.Domain.Entities
{
    public class ServiceHistory
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Cost { get; set; }

        // ✅ Many Service Histories -> One Vehicle
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
    }
}
