namespace App.DTOs.Auth
{
    public sealed record LoginResponse(string AccessToken, DateTimeOffset ExpiresAt);
}
