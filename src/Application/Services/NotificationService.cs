// ./src/Application/Services/NotificationService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUserRepository _userRepository;

        public NotificationService(
            INotificationRepository notificationRepository,
            IUserRepository userRepository)
        {
            _notificationRepository = notificationRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<NotificationDTO>> GetUserNotificationsAsync(int userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserAsync(userId);
            return notifications.Select(n => new NotificationDTO
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                CreatedAt = n.CreatedAt,
                IsRead = n.IsRead
            });
        }

        public async Task SendNotificationAsync(int userId, string title, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task SendPromotionalNotificationAsync(string title, string message, List<int>? userIds = null)
        {
            // If userIds is null, send to all users
            var targetUserIds = userIds ?? (await _userRepository.GetAllAsync()).Select(u => u.Id).ToList();

            foreach (var userId in targetUserIds)
            {
                var notification = new Notification
                {
                    UserId = userId,
                    Title = title,
                    Message = message,
                    CreatedAt = DateTime.UtcNow
                };
                await _notificationRepository.AddNotificationAsync(notification);
            }
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            await _notificationRepository.MarkAsReadAsync(notificationId);
        }
    }
}