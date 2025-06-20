namespace MyApi.Models;

/// <summary>
/// Session model for representing rows in database table
/// </summary>
public record class Session
{
    public int Id { get; init; } //PK
    public string? SessionID { get; init; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }
}