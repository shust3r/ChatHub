using ChatHub.API.Models;
using Dapper;

namespace ChatHub.API.Services;

public class ChatService : IChatService
{
    private readonly SqliteConnectionFactory _connection;
    private readonly IDictionary<string, int> _chatIds;

    public ChatService(IDictionary<string, int> chatIds,
        SqliteConnectionFactory connectionFactory)
    {
        _connection = connectionFactory;
        _chatIds = chatIds;
    }

    public async Task<int> CreateChat(string chatName)
    {
        var connection = _connection.Create();

        const string insertQuery = """
            INSERT INTO Chat (Name) VALUES (@chatName);
            """;

        await connection.ExecuteAsync(insertQuery, new { chatName });

        const string getQuery = """
            SELECT id
            FROM Chat
            WHERE Chat.Name = @chatName
            ORDER BY id DESC
            LIMIT 1;
            """;

        return await connection.QuerySingleOrDefaultAsync<int>(getQuery, new { chatName });
    }

    public async Task AddJoined(string chatName, string userName, DateTime messageTime)
    {
        if (!_chatIds.ContainsKey(chatName))
        {
            int chatId = await CreateChat(chatName);
            _chatIds.Add(chatName, chatId);
        }

        var message = new MessageDto
        {
            UserName = "System",
            Content = $"{userName} joined the Chat",
            MessageTime = messageTime,
            ChatId = _chatIds[chatName]
        };

        var connection = _connection.Create();

        const string query = """
            INSERT INTO Message (UserName, Content, MessageTime, Chat_id)
            VALUES (@UserName, @Content, @MessageTime, @ChatId);
            """;

        await connection.ExecuteAsync(query, message);
    }

    public async Task AddMessage(string chatName, string userName, string message, DateTime messageTime)
    {
        var newMessage = new MessageDto
        {
            UserName = userName,
            Content = message,
            MessageTime = messageTime,
            ChatId = _chatIds[chatName]
        };

        var connection = _connection.Create();

        const string query = """
            INSERT INTO Message (UserName, Content, MessageTime, Chat_id)
            VALUES (@UserName, @Content, @MessageTime, @ChatId);
            """;

        await connection.ExecuteAsync(query, newMessage);
    }

    public async Task AddLeft(string chatName, string userName, DateTime messageTime, bool isLastUser)
    {
        var message = new MessageDto
        {
            UserName = "System",
            Content = $"{userName} left the Chat",
            MessageTime = messageTime,
            ChatId = _chatIds[chatName]
        };

        var connection = _connection.Create();

        const string query = """
            INSERT INTO Message (UserName, Content, MessageTime, Chat_id)
            VALUES (@UserName, @Content, @MessageTime, @ChatId);
            """;

        await connection.ExecuteAsync(query, message);

        if (isLastUser)
            _chatIds.Remove(chatName);
    }
}