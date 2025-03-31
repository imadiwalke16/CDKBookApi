using System;
namespace Application.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }  // ✅ Should be int, not Guid
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }


}

