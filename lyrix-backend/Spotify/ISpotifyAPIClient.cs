namespace Backend.Spotify;

public interface ISpotifyAPIClient
{
    Task<string> AccessCodeQuery(string uniqueID);
    //Task<string> getTokens();
    //Task <IDictionary<string, string>> refreshAccess();
}