namespace Backend.Spotify;

internal class SpotifyAPIClient: ISpotifyAPIClient
{
    private string ClientId;
    private string ClientSecret;
    private string RedirectUri;
    private HttpClient http;
    private string CodeChallenge;
    private string CodeVerifier;

    public SpotifyAPIClient(
        HttpClient http,
        IConfiguration cfg)
    {
        this.ClientId = cfg["Spotify:ClientID"]
                            ?? throw new ArgumentNullException("ClientID is missing");
        this.RedirectUri = cfg["Spotify:RedirectURI"]
                            ?? throw new ArgumentNullException("RedirectURI is missing");
        this.http = http;
        this.ClientSecret = cfg["Spotify:ClientSecret"]
                            ?? throw new ArgumentException("ClientSecret is missing");
        this.CodeVerifier = PkceHelper.GenerateRandString(60);
        this.CodeChallenge = PkceHelper.Base64Encode(PkceHelper.Sha256(this.CodeVerifier));

    }
    //non-static methods are for when you want to refer to object state.
    public Task<string> AccessCodeQuery(string uniqueID)
    {
        string codeVerifier = PkceHelper.GenerateRandString(60);

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