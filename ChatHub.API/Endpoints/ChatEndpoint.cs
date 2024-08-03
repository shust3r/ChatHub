using ChatHub.API.Entities;
using ChatHub.API.Services;
using Dapper;

namespace ChatHub.API.Endpoints;

public static class ChatEndpoint
{
    public static void MapChatEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapHub<Hub.ChatHub>("/chat");

        var group = builder.MapGroup("chats");

        group.MapGet("", async (SqliteConnectionFactory sqliteConnection) =>
        {
            using var connection = sqliteConnection.Create();

            const string query = """
                SELECT *
                FROM Chat
            """;

            var chats = await connection.QueryAsync<Chat>(query);

            return Results.Ok(chats);
        });

        group.MapGet("{id}", async (int id, SqliteConnectionFactory sqliteConnection) =>
        {
            using var connection = sqliteConnection.Create();

            const string query = """
                SELECT Chat.id AS ChatId, Chat.Name AS ChatName, Message.id, Message.UserName, Message.Content, Message.MessageTime
                FROM Chat
                INNER JOIN Message
                ON Chat.id = Message.Chat_id
                WHERE Chat.id = @id
                ORDER BY Message.MessageTime ASC
            """;

            var messages = await connection.QueryAsync<Message>(
                query, new { id });

            return messages is not null ? Results.Ok(messages) : Results.NotFound();
        });

        group.MapDelete("{id}", async (int id, SqliteConnectionFactory sqliteConnection) =>
        {
            using var connection = sqliteConnection.Create();

            const string query = """
                DELETE FROM Chat
                WHERE Chat.id = @id
            """;

            await connection.ExecuteAsync(query,
                new { id });

            return Results.NoContent();
        });
    }
}
