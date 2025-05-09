using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

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

        [Required]
        [MaxLength(50)]
        public string CustomerId { get; set; } = string.Empty;

        [ForeignKey("Dealer")]
        public int? DealerId { get; set; } // Nullable

        public Dealer? Dealer { get; set; } // Nullable navigation property

        public List<Vehicle> Vehicles { get; set; } = new();
        public List<ServiceAppointment> ServiceAppointments { get; set; } = new();
        public List<Notification> Notifications { get; set; } = new();
    }
}