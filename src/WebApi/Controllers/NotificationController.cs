// ./src/WebApi/Controllers/NotificationController.cs
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApi.Hubs;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IUserRepository _userRepository; // Added for fetching users

        public NotificationController(
            NotificationService notificationService,
            IHubContext<NotificationHub> hubContext,
            IUserRepository userRepository)
        {
            _notificationService = notificationService;
            _hubContext = hubContext;
            _userRepository = userRepository;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var notifications = await _notificationService.GetUserNotificationsAsync(userId);
            return Ok(notifications);
        }

        [HttpPost]
        public async Task<IActionResult> SendNotification([FromBody] NotificationDTO notificationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (notificationDto.Id == 0)
                return BadRequest("User ID is required.");

            await _notificationService.SendNotificationAsync(notificationDto.Id, notificationDto.Title, notificationDto.Message);
            await _hubContext.Clients.User(notificationDto.Id.ToString())
                .SendAsync("ReceiveNotification", notificationDto.Id, notificationDto.Title, notificationDto.Message);

            return Ok("Notification sent successfully.");
        }

        [HttpPost("promotional")]
        public async Task<IActionResult> SendPromotionalNotification([FromBody] PromotionalNotificationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _notificationService.SendPromotionalNotificationAsync(request.Title, request.Message, request.UserIds);

            // Broadcast to all specified users (or all if UserIds is null)
            var targetUsers = request.UserIds ?? (await _userRepository.GetAllAsync()).Select(u => u.Id).ToList();
            foreach (var userId in targetUsers)
            {
                await _hubContext.Clients.User(userId.ToString())
                    .SendAsync("ReceiveNotification", userId, request.Title, request.Message);
            }

            return Ok("Promotional notification sent successfully.");
        }

        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkNotificationAsRead(int id)
        {
            await _notificationService.MarkNotificationAsReadAsync(id);
            return Ok("Notification marked as read.");
        }
    }

    public class PromotionalNotificationRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public List<int>? UserIds { get; set; }
    }
}