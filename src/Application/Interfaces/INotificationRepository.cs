using System;
using Application.Domain.Entities;

namespace Application.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserAsync(int userId);  // ✅ Change to int
        Task AddNotificationAsync(Notification notification);
        Task MarkAsReadAsync(int notificationId);  // ✅ Change to int
    }

}

