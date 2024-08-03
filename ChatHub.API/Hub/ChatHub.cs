using ChatHub.API.Models;
using ChatHub.API.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatHub.API.Hub;

public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IDictionary<string, UserRoomConnection> _connection;
    private readonly IChatService _chatService;

    public ChatHub(IDictionary<string, UserRoomConnection> connection,
        IChatService chatService)
    {
        _connection = connection;
        _chatService = chatService;
    }

    public async Task JoinRoom(UserRoomConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomName!);
        _connection[Context.ConnectionId] = userConnection;
        await Clients.Groups(userConnection.RoomName!)
            .SendAsync("ReceiveMessage", "Bot", $"{userConnection.UserName} has joined the Chat ", DateTime.UtcNow);
        await SendConnectedUser(userConnection.RoomName!);
        
        await _chatService.AddJoined(userConnection.RoomName!, userConnection.UserName!, DateTime.UtcNow);
    }

    public async Task SendMessage(string message)
    {
        if(_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
        {
            await Clients.Group(userRoomConnection.RoomName!)
                .SendAsync("ReceiveMessage", userRoomConnection.UserName, message, DateTime.UtcNow);

            await _chatService.AddMessage(userRoomConnection.RoomName!, userRoomConnection.UserName!, message, DateTime.UtcNow);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (!_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection connection))
        {
            return base.OnDisconnectedAsync(exception);
        }
        _connection.Remove(Context.ConnectionId);
        Clients.Group(connection.RoomName!)
            .SendAsync("ReceiveMessage", "Bot", $"{connection.UserName} has left the Chat", DateTime.UtcNow);

        _chatService.AddLeft(connection.RoomName!, connection.UserName!, DateTime.UtcNow, IsLastUser(connection.RoomName!));

        SendConnectedUser(connection.RoomName!);
        return base.OnDisconnectedAsync(exception);
    }

    public Task SendConnectedUser(string room)
    {
        var users = _connection.Values
            .Where(u => u.RoomName == room)
            .Select(r => r.UserName);
        return Clients.Group(room).SendAsync("ConnectedUser", users);
    }

    public bool IsLastUser(string room)
    {
        return _connection.Values
            .Count(u => u.RoomName == room) == 1;
    }
}
