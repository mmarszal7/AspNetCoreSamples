using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    [Authorize(Roles = "Admin")]
    public class AdvancedHub : Hub
    {
        public void BroadcastMessage(string name, string message)
        {
            Clients.All.SendAsync(name, message);
        }

        public void Echo(string name, string message)
        {
            Clients.Client(Context.ConnectionId).SendAsync(name, message + " (echo from server)");
        }

        public async Task JoinGroup(string name, string groupName)
        {
            var addition = Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var sending = Clients.Group(groupName).SendAsync("_SYSTEM_", $"{name} joined {groupName} with connectionId {Context.ConnectionId}");

            await Task.WhenAll(addition, sending).ConfigureAwait(false);
        }

        public async Task LeaveGroup(string name, string groupName)
        {
            var removal = Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            var clientSending = Clients.Client(Context.ConnectionId).SendAsync("_SYSTEM_", $"{name} leaved {groupName}");
            var groupSending = Clients.Group(groupName).SendAsync("_SYSTEM_", $"{name} leaved {groupName}");

            await Task.WhenAll(removal, clientSending, groupSending).ConfigureAwait(false);
        }

        public void SendGroup(string name, string groupName, string message)
        {
            Clients.Group(groupName).SendAsync(name, message);
        }

        public void SendGroups(string name, IReadOnlyList<string> groups, string message)
        {
            Clients.Groups(groups).SendAsync(name, message);
        }

        public void SendUser(string name, string userId, string message)
        {
            Clients.User(userId).SendAsync(name, message);
        }

        public void SendUsers(string name, IReadOnlyList<string> userIds, string message)
        {
            Clients.Users(userIds).SendAsync(name, message);
        }
    }
}


