using Chatbo.Api.Hubs;
using Microsoft.AspNet.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var hubConfiguration = new HubConfiguration();
hubConfiguration.EnableDetailedErrors = true;
hubConfiguration.EnableJavaScriptProxies = false;

app.MapHub<ChatRoomHub>(ChatRoomHub.HubUrl);

app.Run();
