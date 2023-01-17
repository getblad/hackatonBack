using Microsoft.AspNetCore.SignalR;

namespace RestApiASPNET.Hubs;

public class UserHub:Hub
{
    public async Task SendUserInfo(string user, string message)
    {
        
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}