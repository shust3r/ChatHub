using Microsoft.Data.Sqlite;

namespace ChatHub.API.Services;

public class SqliteConnectionFactory
{
    private readonly string _connectionString;

    public SqliteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqliteConnection Create()
    {
        return new SqliteConnection(_connectionString);
    }
}
