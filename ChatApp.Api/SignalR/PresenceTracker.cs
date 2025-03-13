namespace ChatApp.Api.SignalR
{
    public class PresenceTracker
    {
        private readonly Dictionary<string, List<string>> OnlineUsers = new Dictionary<string, List<string>>();
        public Task UserConnected(string userName, string UserConnectionId)
        {
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(userName))
                {
                    OnlineUsers[userName].Add(UserConnectionId);
                }
                else
                {
                    OnlineUsers.Add(userName, new List<string> { UserConnectionId });
                }
            }
            return Task.CompletedTask;
        }
        public Task UserDisConnected(string userName, string UserConnectionId)
        {
            lock (OnlineUsers)
            {
                if (!OnlineUsers.ContainsKey(userName)) return Task.CompletedTask;
                OnlineUsers[userName].Remove(UserConnectionId);
                if (OnlineUsers[userName].Count == 0)
                {
                    OnlineUsers.Remove(userName);
                }

            }

            return Task.CompletedTask;
        }
        public Task<string[]> GetOnlineUsers()
        {
            string[] onlineUsers;
            lock (OnlineUsers)
            {
                onlineUsers=OnlineUsers.OrderBy(k=>k.Key).Select(k=>k.Key).ToArray();
            }
            return Task.FromResult(onlineUsers); 
        }
    }
}
