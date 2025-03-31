// ./src/WebApi/Services/PromotionalNotificationService.cs
using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Hubs;

namespace WebApi.Services
{
    public class PromotionalNotificationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly Random _random; // Moved Random to class scope
        private static readonly (string Title, string Message)[] Promos = new[]
        {
            ("Special Offer!", "Get 20% off your next service this week!"),
            ("Limited Time Deal!", "Free inspection with any booking today!"),
            ("Weekend Promo!", "Book now and save 15% on all services!")
        };

        public PromotionalNotificationService(
            IServiceProvider serviceProvider,
            IHubContext<NotificationHub> hubContext)
        {
            _serviceProvider = serviceProvider;
            _hubContext = hubContext;
            _random = new Random(); // Initialize once in constructor
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var notificationService = scope.ServiceProvider.GetRequiredService<NotificationService>();
                        var promo = Promos[_random.Next(Promos.Length)];

                        // Send to all users dynamically
                        await notificationService.SendPromotionalNotificationAsync(promo.Title, promo.Message);

                        // Broadcast to all connected clients
                        await _hubContext.Clients.All.SendAsync("ReceiveNotification", 0, promo.Title, promo.Message);
                    }

                    // Wait for a random interval (e.g., every 1-24 hours)
                    int delayHours = _random.Next(1, 24); // Use class-level _random
                    await Task.Delay(TimeSpan.FromHours(delayHours), stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in PromotionalNotificationService: {ex.Message}");
                }
            }
        }
    }
}