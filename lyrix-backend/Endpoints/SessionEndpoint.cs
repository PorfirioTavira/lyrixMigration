using Backend.Data;
using Backend.Models;
using Backend.Spotify;
using Backend.Dto;
namespace Backend.Endpoints;

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

        group.MapGet("/getAccessCode", GetAccessCode
       ).Produces<AccessCodeDto>(StatusCodes.Status200OK)
       .Produces<AccessCodeDto>(StatusCodes.Status400BadRequest);

        return group;

    }
    private static async Task<AccessCodeDto> GetAccessCode(ISpotifyAPIClient spotify, string uniqueID)
    {
        string queryString = await spotify.AccessCodeQuery(uniqueID);
        return await Task.FromResult<AccessCodeDto>(new AccessCodeDto(uniqueID, queryString));
    }
    
}
//The DTO seems to be the Data Transfer Object that is used to pass in the argumgents for the object and to post to the database.
//public record SessionCreateDto(string SessionID);