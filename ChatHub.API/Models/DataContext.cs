using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace ChatHub.API.Models;

public class DataContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(Configuration.GetConnectionString("WebApiDatabase"));
    }

    public async Task InitDB()
    {
        using var connection = CreateConnection();
        await _initChats();
        

        async Task _initChats()
        {
            var query = """
                CREATE TABLE IF NOT EXISTS
                Chat (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS
                Message (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    UserName TEXT NOT NULL,
                    Content TEXT,
                    MessageTime TEXT,
                    Sentiment TEXT,
                    PositiveScore REAL,
                    NeutralScore REAL,
                    NegativeScore REAL,
                    Chat_id INTEGER,
                    FOREIGN KEY(Chat_id) REFERENCES Chat(id) ON DELETE CASCADE
                );
                """;

            await connection.ExecuteAsync(query);
        }
    }
}
