using System;
namespace Application.DTOs.Vehicle
{
    public class VehicleDTO
    {
        public int Id { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Year { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;
    }
}
