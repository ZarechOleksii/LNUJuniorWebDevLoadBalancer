using Microsoft.AspNetCore.SignalR;

namespace LoadBalancer.Hubs
{
    public class StatusHub : Hub
    {
        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }
    }
}