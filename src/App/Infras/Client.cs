using App.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Infras
{
    public static class Client
    {
        private static HttpClient _http = new();
        private static string? _accessToken;
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

            res.EnsureSuccessStatusCode();

            var dto = await res.Content.ReadFromJsonAsync<LoginResponse>() ?? throw new Exception("Invalid login response.");

            _accessToken = dto.AccessToken;
            _expiresAt = dto.ExpiresAt;
        }

        public static void Logout()
        {
            ClearToken();
            LoggedOut?.Invoke();
        }

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
                Logout();
                throw new UnauthorizedAccessException();
            }

            res.EnsureSuccessStatusCode();
            return res; // caller must dispose
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
            _expiresAt = null;
        }
    }
}
