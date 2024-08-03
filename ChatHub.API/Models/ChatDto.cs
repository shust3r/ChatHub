using ChatHub.API.Entities;

namespace ChatHub.API.Models;

public record ChatDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<MessageDto> Messages { get; set; }
        = new List<MessageDto>();

    public static ChatDto FromChat(Chat chat)
    {
        return new ChatDto
        {
            Id = chat.Id,
            Name = chat.Name,
            Messages = chat.Messages
                .Select(m => MessageDto.FromMessage(m))
                .ToList()
        };
    }

    public Chat ToChat()
    {
        return new Chat
        {
            Id = Id,
            Name = Name,
            Messages = Messages
                .Select(m => m.ToMessage())
                .ToList()
        };
    }
}