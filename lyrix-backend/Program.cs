using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Endpoints;
using Backend.Spotfiy;
using dotenv.net;
using System.Security.Permissions;
using DotNetEnv.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var envVars = DotNetEnv.Env.NoEnvVars().TraversePath().Load().ToDotEnvDictionary();
var code = PkceHelper.GenerateRandString(60);
var hashed = PkceHelper.Sha256(code);
var codeChallenge = PkceHelper.Base64Encode(hashed);
var spotifyApiClient = new SpotifyAPIClient(envVars["CLIENT_ID"],envVars["CLIENT_SECRET"],envVars["REDIRECT_URI"], code, codeChallenge);
var connectionString =
    builder.Configuration.GetConnectionString("Default")
        ?? throw new InvalidOperationException("Connection string"
        + "'Default' not found.");
Console.WriteLine(await spotifyApiClient.AccessCodeQuery("1234567"));
builder.Services.AddDbContext<SessionDbContext>(options =>
    options.UseSqlServer(connectionString));

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

