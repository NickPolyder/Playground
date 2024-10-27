using Microsoft.AspNetCore.SignalR;
using Playground.SignalR.Shared;

namespace Playground.SignalR.Server.Hubs
{
    public class ChatHub : Hub<IChatServer>, IChatClient
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public Task SendMessageToCaller(string user, string message)
        {
            return Clients.Caller.ReceiveMessage(user, message);
        }
    }
}
