using Backend_core.Classes;
using Backend_core.Interfaces;
using Backend_DAL.Classes;
using System.Net;
using System.Net.WebSockets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddScoped<IPlaylistRepository, PlaylistRepository>();
builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IShowRepository, ShowRepository>();

/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .WithMethods("GET", "POST")
                .AllowCredentials();
        });
});*/

var app = builder.Build();


app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWebSockets();

/*app.Map("/wsC", async context =>
{


    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

        WebsocketTestService.addTestSockets(webSocket);

        // We have to hold the context here if we release it, server will close it
        while (webSocket.State == WebSocketState.Open)
        {
            await Task.Delay(TimeSpan.FromMinutes(1));
        }

        // if socket status is not open ,remove it
        WebsocketTestService.removeTestSockets(webSocket);

        // check socket state if it is not closed, close it
        if (webSocket.State != WebSocketState.Closed && webSocket.State != WebSocketState.Aborted)
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Connection End", CancellationToken.None);
        }
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});*/
    
app.MapControllers();
app.MapHub<WebsocketService>("/wsHub");
    
app.Run();
