using Microsoft.AspNetCore.SignalR;

namespace WebApplication3.Hubs
{
    public class GameHub : Hub
    {

        public async Task SendMessage(string user, string message)
        {
            Clients.All.SendAsync("ReceiveMessage", user, message);   
        }
    }
}
