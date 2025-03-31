using System;
// ./src/WebApi/Hubs/NotificationHub.cs
// ./src/WebApi/Hubs/NotificationHub.cs
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(int userId, string title, string message)
        {
            await Clients.User(userId.ToString()).SendAsync("ReceiveNotification", userId, title, message);
        }

        public override Task OnConnectedAsync()
        {
            // Optionally, associate ConnectionId with UserId if using authentication
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}