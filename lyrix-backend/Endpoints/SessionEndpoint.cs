using Microsoft.OpenApi.Validations;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Endpoints;

public static class SessionEndpoints
{
    public static RouteGroupBuilder MapSessionEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/", async (SessionDbContext db, SessionCreateDto dto) =>
        {
            var session = new Session { SessionID = dto.SessionID };
            db.Sessions.Add(session);
            await db.SaveChangesAsync();

            return Results.Created($"/sessions/{session.Id}", session);
        })
        .Produces<Session>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        return group;
    }
}

public record SessionCreateDto(string SessionID);