using ChatHub.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IDictionary<string, UserRoomConnection>>(op =>
    new Dictionary<string,UserRoomConnection>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<ChatHub.API.Hub.ChatHub>("/chat");
});

app.MapControllers();

app.Run();
