using Application.Domain.Entities;

public class ServiceOffered
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;  // Example: "Oil Change"
    public decimal Price { get; set; } // Example: ₹500

    // ✅ Many Services → One Service Center
    public int ServiceCenterId { get; set; }
    public ServiceCenter ServiceCenter { get; set; } = null!;

    // ✅ Many-to-Many: Services Linked to Appointments
    public List<ServiceAppointmentService> ServiceAppointmentServices { get; set; } = new();
    //public string ServiceName { get; set; } = string.Empty; // ✅ Add this property

}
