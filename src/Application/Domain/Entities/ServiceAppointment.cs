using Application.Domain.Entities;

public class ServiceAppointment
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = "Scheduled";

    // ✅ Many Appointments -> One User
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    // ✅ Many Appointments -> One Vehicle
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; } = null!;

    // ✅ Many Appointments -> One Service Center
    public int ServiceCenterId { get; set; }
    public ServiceCenter ServiceCenter { get; set; } = null!;

    // ✅ Transport Mode & Extra Charge
    public string TransportMode { get; set; } = "Self-Drive"; // "Pickup" / "Drop-Off"
    public decimal TransportCharge { get; set; } = 0; // Extra charge for pickup/drop

    // ✅ Many-to-Many: Appointment ↔ Services Selected
    public List<ServiceAppointmentService> ServiceAppointmentServices { get; set; } = new();
}
