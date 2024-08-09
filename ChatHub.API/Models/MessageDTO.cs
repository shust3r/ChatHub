using ChatHub.API.Entities;

namespace ChatHub.API.Models;

public class MessageDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Sentiment { get; set; } = null!;
    public double PositiveScore { get; set; }
    public double NeutralScore { get; set; }
    public double NegativeScore { get; set; }
    public DateTime MessageTime { get; set; }
    public int ChatId { get; set; }

    public static MessageDto FromMessage(Message message)
    {
        return new MessageDto
        {
            Id = message.Id,
            UserName = message.UserName,
            Content = message.Content,
            Sentiment = message.Sentiment,
            PositiveScore = message.PositiveScore,
            NeutralScore = message.NeutralScore,
            NegativeScore = message.NegativeScore,
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
            Sentiment = Sentiment,
            PositiveScore = PositiveScore,
            NeutralScore = NeutralScore,
            NegativeScore = NegativeScore,
            MessageTime = MessageTime,
            ChatId = ChatId
        };
    }
}
