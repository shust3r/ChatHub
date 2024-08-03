using ChatHub.API.Entities;

namespace ChatHub.API.Models;

public class MessageDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime MessageTime { get; set; }
    public int ChatId { get; set; }

    public static MessageDto FromMessage(Message message)
    {
        return new MessageDto
        {
            Id = message.Id,
            UserName = message.UserName,
            Content = message.Content,
            MessageTime = message.MessageTime,
            ChatId = message.ChatId
        };
    }

    public Message ToMessage()
    {
        return new Message
        {
            Id = Id,
            UserName = UserName,
            Content = Content,
            MessageTime = MessageTime,
            ChatId = ChatId
        };
    }
}
