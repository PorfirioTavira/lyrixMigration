using System.Text;
using System.Text.Json;
using System.Net.Http;
using Microsoft.OpenApi.Models;
using System.Web;
using System.Collections.Generic;
namespace Backend.Spotfiy;

class SpotifyAPIClient: ISpotifyAPIClient
{
    private string ClientId;
    private string ClientSecret;
    private string RedirectUri;
    private string CodeVerifier;
    private string CodeChallenge;
    public SpotifyAPIClient(string ClientId, string ClientSecret, string RedirectUri, string CodeVerifier, string CodeChallenge)
    {
        this.ClientId = ClientId;
        this.ClientSecret = ClientSecret;
        this.RedirectUri = RedirectUri;
        this.CodeVerifier = CodeVerifier;
        this.CodeChallenge = CodeChallenge;
    }
    //non-static methods are for when you want to refer to object state.
    public Task<string> AccessCodeQuery(string uniqueID)
    {

        var queryParams = new Dictionary<string, string>
        {
            {"client_id", this.ClientId },
            {"response_type", "code" },
            {"redirect_uri" , this.RedirectUri},
            {"scope" , "user-read-recently-played" },
            {"state" , uniqueID },
            {"code_challenge_method" , "S256" },
            {"code_challenge" , this.CodeChallenge }
        };
        string queryString = string.Join("&", queryParams.Select(kvp => $"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}"));
        queryString = $"https://accounts.spotify.com/authorize?{queryString}";
        return Task.FromResult(queryString);


    }
}