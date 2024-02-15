using BussinessObject.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace viecvat247API.Hubs
{
    [Authorize]
    public class NotificationHub : Hub
    {
        private static readonly Dictionary<string, string> ConnectedUsers = new Dictionary<string, string>();


        public override Task OnConnectedAsync()
        {
            var userId = Context.User.Claims.FirstOrDefault(c => c.Type == "NameIdentifier")?.Value;
            if (!string.IsNullOrEmpty(userId))
            {
                ConnectedUsers[userId] = Context.ConnectionId;
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId) && ConnectedUsers.ContainsKey(userId))
            {
                ConnectedUsers.Remove(userId);
            }

            return base.OnDisconnectedAsync(exception);
        }

        public static async Task SendNotification(IHubContext<NotificationHub> _hubContext, string userId, string description)
        {
            if (ConnectedUsers.TryGetValue(userId, out var connectionId))
            {
                    await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveNotification", description);
            }
        }

        public static async Task UpdateNotification(IHubContext<NotificationHub> _hubContext, string userId)
        {
            if (ConnectedUsers.TryGetValue(userId, out var connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("UpdateNotification");
            }
        }

    }
}
