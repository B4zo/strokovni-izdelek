using App.DTOs.Auth;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace App.Infras;

public static class Client
{
    private static HttpClient _http = new();
    private static string? _accessToken;
    private static string? _refreshToken;
    private static DateTimeOffset? _expiresAt;

    public static event Action? LoggedOut;

    public static bool IsLoggedIn =>
        !string.IsNullOrWhiteSpace(_accessToken) &&
        _expiresAt is not null &&
        _expiresAt.Value > DateTimeOffset.UtcNow;

    public static void UseBaseUrl(string baseUrl)
    {
        _http = new HttpClient
        {
            BaseAddress = new Uri(baseUrl),
            Timeout = TimeSpan.FromSeconds(20)
        };

        ClearToken();
    }

    public static async Task LoginAsync(string username, string password)
    {
        var payload = new LoginRequest(username, password);

        using var res = await _http.PostAsJsonAsync("auth/login", payload);
        if (res.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException("Invalid credentials.");
        if (res.StatusCode == HttpStatusCode.Conflict)
            throw new InvalidOperationException("User already has an active session.");

        res.EnsureSuccessStatusCode();

        var dto = await res.Content.ReadFromJsonAsync<LoginResponse>()
            ?? throw new Exception("Invalid login response.");

        _accessToken = dto.AccessToken;
        _expiresAt = dto.ExpiresAt;
        _refreshToken = dto.RefreshToken;
    }

    public static async Task<bool> TryRefreshAsync()
    {
        if (string.IsNullOrWhiteSpace(_refreshToken))
            return false;

        using var res = await _http.PostAsJsonAsync("auth/refresh", new RefreshRequest(_refreshToken));
        if (!res.IsSuccessStatusCode)
            return false;

        var dto = await res.Content.ReadFromJsonAsync<LoginResponse>()
            ?? throw new Exception("Invalid refresh response.");

        _accessToken = dto.AccessToken;
        _expiresAt = dto.ExpiresAt;
        _refreshToken = dto.RefreshToken;
        return true;
    }

    public static async Task LogoutAsync(bool suppressLoggedOutEvent = false)
    {
        if (!string.IsNullOrWhiteSpace(_refreshToken))
        {
            try
            {
                using var req = new HttpRequestMessage(HttpMethod.Post, "auth/logout")
                {
                    Content = JsonContent.Create(new LogoutRequest(_refreshToken))
                };

                if (!string.IsNullOrWhiteSpace(_accessToken))
                    req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                await _http.SendAsync(req);
            }
            catch
            {
                // best effort
            }
        }

        ClearToken();
        if (!suppressLoggedOutEvent)
            LoggedOut?.Invoke();
    }

    public static void Logout() => LogoutAsync().GetAwaiter().GetResult();

    private static async Task<HttpResponseMessage> SendCoreAsync(HttpMethod method, string url, object? body)
    {
        using var req = new HttpRequestMessage(method, url);

        if (IsLoggedIn)
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

        if (body is not null)
            req.Content = JsonContent.Create(body);

        var res = await _http.SendAsync(req);
        if (res.StatusCode == HttpStatusCode.Unauthorized)
        {
            res.Dispose();
            if (await TryRefreshAsync())
            {
                using var retryReq = new HttpRequestMessage(method, url);
                retryReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                if (body is not null)
                    retryReq.Content = JsonContent.Create(body);

                var retryRes = await _http.SendAsync(retryReq);
                if (retryRes.StatusCode == HttpStatusCode.Unauthorized)
                {
                    retryRes.Dispose();
                    await LogoutAsync();
                    throw new UnauthorizedAccessException();
                }

                retryRes.EnsureSuccessStatusCode();
                return retryRes;
            }

            await LogoutAsync();
            throw new UnauthorizedAccessException();
        }

        res.EnsureSuccessStatusCode();
        return res;
    }

    public static async Task SendAsync(HttpMethod method, string url, object? body = null)
    {
        using var res = await SendCoreAsync(method, url, body);
    }

    public static async Task<TResponse> SendAsync<TResponse>(HttpMethod method, string url, object? body = null)
    {
        using var res = await SendCoreAsync(method, url, body);
        var dto = await res.Content.ReadFromJsonAsync<TResponse>(new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return dto ?? throw new Exception("Invalid JSON response.");
    }

    private static void ClearToken()
    {
        _accessToken = null;
        _refreshToken = null;
        _expiresAt = null;
    }
}
