using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Hubs;

public sealed class ChatHub : Hub
{
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} has connected"); 
        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined");
        Console.WriteLine($"Sent join message for {Context.ConnectionId}");
    }

    public async Task SendNotification(string content)
    {
        await Clients.All.SendAsync("ReceiveNotification", content);
    }

    //public Task Echo(string name, string message)
    //{
    //    return Clients.Client(Context.ConnectionId)
    //        .SendAsync("echo", name, $"{message} (echo from server)");
    //}
}