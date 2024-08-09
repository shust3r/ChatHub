using Azure.AI.TextAnalytics;

namespace ChatHub.API.Services
{
    public interface IChatService
    {
        Task AddJoined(string chatName, string userName, DateTime messageTime);
        Task AddLeft(string chatName, string userName, DateTime messageTime, bool isLastUser);
        Task AddMessage(string chatName, string userName, string message, DocumentSentiment sentiment, DateTime messageTime);
        Task<int> CreateChat(string chatName);
    }
}