using ChatHub.API.Models;
using ChatHub.API.Services;

namespace ChatHub.API.Configurations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IDictionary<string, UserRoomConnection>>(op => 
            new Dictionary<string, UserRoomConnection>());

        services.AddSingleton<IDictionary<string, int>>(o =>
            new Dictionary<string, int>());

        services.AddSingleton<IChatService, ChatService>();

        services.AddSingleton(serviceProvider =>
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            var connectionString = config.GetConnectionString("WebApiDatabase")
                ?? throw new ApplicationException("The connection string is null");

            return new SqliteConnectionFactory(connectionString);
        });

        services.AddSingleton<DataContext>();

        services.AddCors(o =>
        {
            o.AddDefaultPolicy(builder =>
            {
                builder.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });

        return services;
    }
}
