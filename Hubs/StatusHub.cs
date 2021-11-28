using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

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