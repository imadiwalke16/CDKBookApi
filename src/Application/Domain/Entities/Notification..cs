﻿using System;

namespace Application.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;  // ✅ Add this

        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;

        // ✅ Many Notifications -> One User
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
