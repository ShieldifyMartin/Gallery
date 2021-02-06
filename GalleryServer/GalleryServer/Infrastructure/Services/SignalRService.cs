using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace GalleryServer.Infrastructure.Services
{
    public class SignalRService : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
