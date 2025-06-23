using Microsoft.OpenApi.Validations;
using MyApi.Data;
using MyApi.Models; 
namespace MyApi.Endpoints;

public static class SessionEndpoints
{
    public static RouteGroupBuilder MapSessionEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/setSession", async (SessionDbContext db) =>
        {
            var session = new Session
            {
                SessionID = Guid.NewGuid().ToString()
            };
            db.Sessions.Add(session);
            await db.SaveChangesAsync();

            return Results.Created($"/sessions/", session);
        })
        .Produces<Session>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);
/*         group.MapGet("/getAccessCode", async (SessionDbContext db) =>
        {

        }
        ).Produces<Session>(StatusCodes.Status200OK)
        .Produces<Session>(StatusCodes.Status400BadRequest);
        */
        return group;
    }
}
//The DTO seems to be the Data Transfer Object that is used to pass in the argumgents for the object and to post to the database.
//public record SessionCreateDto(string SessionID);