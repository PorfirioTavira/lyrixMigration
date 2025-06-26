using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Endpoints;
using Backend.Spotify;
using System.Security.Permissions;
using DotNetEnv.Extensions;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
DotNetEnv.Env.TraversePath().Load();
var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

//Setup Database connection string.
var connectionString =
    cfg.GetConnectionString("Default")
        ?? throw new InvalidOperationException("Connection string"
        + "'Default' not found.");
builder.Services.AddDbContext<SessionDbContext>(options =>
    options.UseSqlServer(connectionString));

//Register SpotfiyAPIClient. This will pass in http client, builder.Configuration which contains loaded env vars.
builder.Services.AddHttpClient<ISpotifyAPIClient, SpotifyAPIClient>(http =>
{
    http.BaseAddress = new Uri("https://api.spotify.com/v1/");
});



builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();  // scans minimal APIs
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                        // exposes /swagger/v1/swagger.json
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        options.RoutePrefix = string.Empty;  // root path → http://localhost:5001/
    });
    app.MapOpenApi();
}



app.MapGroup("/sessions")
    .MapSessionEndpoints();
app.UseHttpsRedirection();


app.Run();

