using ChatApp.Api.Data.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.InteropServices;

namespace ChatApp.Api.SignalR
{
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class PresenceHub:Hub
    {
        private readonly PresenceTracker _presenceTracker;
        public PresenceHub(PresenceTracker presenceTracker)
        {
            _presenceTracker = presenceTracker;
        }
        public override async Task OnConnectedAsync()
        {
          await  _presenceTracker.UserConnected(Context.User.GetCurrentUserName(),Context.ConnectionId);
           await Clients.Others.SendAsync("UserIsOnline"+Context.User.GetCurrentUserName());  
            var onlineUsers=_presenceTracker.GetOnlineUsers();
            await Clients.All.SendAsync("OnlineUsers", onlineUsers);
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await _presenceTracker.UserDisConnected(Context.User.GetCurrentUserName(),Context.ConnectionId); 
            await Clients.Others.SendAsync("UserIsOffline" + Context.User.GetCurrentUserName());
            await    base.OnDisconnectedAsync(exception);
            var onlineUsers=_presenceTracker.GetOnlineUsers();
            await Clients.All.SendAsync("OnlineUsers",onlineUsers);
        }
    }
}
