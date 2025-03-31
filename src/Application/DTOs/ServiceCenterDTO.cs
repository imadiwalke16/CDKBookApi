using System;
namespace Application.DTOs.ServiceCenter
{
    public class ServiceCenterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PinCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
