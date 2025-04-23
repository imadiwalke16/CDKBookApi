using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Moq;
using Xunit;

namespace CDKBookAPI.Tests
{
    public class NotificationServiceTests
    {
        private readonly Mock<INotificationRepository> _notificationRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly NotificationService _notificationService;

        public NotificationServiceTests()
        {
            _notificationRepositoryMock = new Mock<INotificationRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _notificationService = new NotificationService(
                _notificationRepositoryMock.Object,
                _userRepositoryMock.Object);
        }

        // Tests for GetUserNotificationsAsync
        [Fact]
        public async Task GetUserNotificationsAsync_ValidUserId_ReturnsNotificationDTOs()
        {
            // Arrange
            int userId = 1;
            var notifications = new List<Notification>
            {
                new Notification { Id = 1, UserId = userId, Title = "Test Title", Message = "Test Message", CreatedAt = DateTime.UtcNow, IsRead = false }
            };
            _notificationRepositoryMock
                .Setup(repo => repo.GetNotificationsByUserAsync(userId))
                .ReturnsAsync(notifications);

            // Act
            var result = await _notificationService.GetUserNotificationsAsync(userId);

            // Assert
            var notificationDtos = result.ToList();
            Assert.Single(notificationDtos);
            Assert.Equal("Test Title", notificationDtos[0].Title);
            Assert.Equal("Test Message", notificationDtos[0].Message);
            Assert.False(notificationDtos[0].IsRead);
        }

        [Fact]
        public async Task GetUserNotificationsAsync_NoNotifications_ReturnsEmptyList()
        {
            // Arrange
            int userId = 1;
            _notificationRepositoryMock
                .Setup(repo => repo.GetNotificationsByUserAsync(userId))
                .ReturnsAsync(new List<Notification>());

            // Act
            var result = await _notificationService.GetUserNotificationsAsync(userId);

            // Assert
            Assert.Empty(result);
        }

        // Tests for SendNotificationAsync
        [Fact]
        public async Task SendNotificationAsync_ValidInput_CallsRepositoryAdd()
        {
            // Arrange
            int userId = 1;
            string title = "New Book";
            string message = "A new book is available!";

            // Act
            await _notificationService.SendNotificationAsync(userId, title, message);

            // Assert
            _notificationRepositoryMock.Verify(
                repo => repo.AddNotificationAsync(It.Is<Notification>(
                    n => n.UserId == userId && n.Title == title && n.Message == message && !n.IsRead)),
                Times.Once());
        }

        [Fact]
        public async Task SendNotificationAsync_EmptyTitle_CallsRepositoryAdd()
        {
            // Arrange
            int userId = 1;
            string title = "";
            string message = "Test Message";

            // Act
            await _notificationService.SendNotificationAsync(userId, title, message);

            // Assert
            _notificationRepositoryMock.Verify(
                repo => repo.AddNotificationAsync(It.Is<Notification>(
                    n => n.UserId == userId && n.Title == "" && n.Message == message)),
                Times.Once());
        }

        // Tests for SendPromotionalNotificationAsync
        [Fact]
        public async Task SendPromotionalNotificationAsync_SpecificUserIds_CallsRepositoryAddForEach()
        {
            // Arrange
            var userIds = new List<int> { 1, 2 };
            string title = "Promotion";
            string message = "Special offer!";
            _userRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<User>());

            // Act
            await _notificationService.SendPromotionalNotificationAsync(title, message, userIds);

            // Assert
            _notificationRepositoryMock.Verify(
                repo => repo.AddNotificationAsync(It.IsAny<Notification>()),
                Times.Exactly(userIds.Count));
            _notificationRepositoryMock.Verify(
                repo => repo.AddNotificationAsync(It.Is<Notification>(
                    n => userIds.Contains(n.UserId) && n.Title == title && n.Message == message)),
                Times.Exactly(userIds.Count));
        }

        [Fact]
        public async Task SendPromotionalNotificationAsync_NullUserIds_SendsToAllUsers()
        {
            // Arrange
            string title = "Promotion";
            string message = "Special offer!";
            var users = new List<User>
            {
                new User { Id = 1 },
                new User { Id = 2 }
            };
            _userRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            await _notificationService.SendPromotionalNotificationAsync(title, message, null);

            // Assert
            _notificationRepositoryMock.Verify(
                repo => repo.AddNotificationAsync(It.IsAny<Notification>()),
                Times.Exactly(users.Count));
            _notificationRepositoryMock.Verify(
                repo => repo.AddNotificationAsync(It.Is<Notification>(
                    n => users.Select(u => u.Id).Contains(n.UserId) && n.Title == title && n.Message == message)),
                Times.Exactly(users.Count));
        }

        [Fact]
        public async Task SendPromotionalNotificationAsync_EmptyUserList_DoesNotCallRepository()
        {
            // Arrange
            string title = "Promotion";
            string message = "Special offer!";
            _userRepositoryMock
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<User>());

            // Act
            await _notificationService.SendPromotionalNotificationAsync(title, message, null);

            // Assert
            _notificationRepositoryMock.Verify(
                repo => repo.AddNotificationAsync(It.IsAny<Notification>()),
                Times.Never());
        }

        // Tests for MarkNotificationAsReadAsync
        [Fact]
        public async Task MarkNotificationAsReadAsync_ValidId_CallsRepositoryMarkAsRead()
        {
            // Arrange
            int notificationId = 1;

            // Act
            await _notificationService.MarkNotificationAsReadAsync(notificationId);

            // Assert
            _notificationRepositoryMock.Verify(
                repo => repo.MarkAsReadAsync(notificationId),
                Times.Once());
        }
    }
}