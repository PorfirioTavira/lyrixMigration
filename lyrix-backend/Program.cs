using Microsoft.EntityFrameworkCore;
using MyApi.Data;
using MyApi.Models;
using MyApi.Endpoints;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
var connectionString =
    builder.Configuration.GetConnectionString("Default")
        ?? throw new InvalidOperationException("Connection string"
        + "'Default' not found.");

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

