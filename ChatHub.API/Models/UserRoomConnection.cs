namespace ChatHub.API.Models;

public record UserRoomConnection
{
    public string? UserName { get; init; }
    public string? RoomName {  get; init; }
}