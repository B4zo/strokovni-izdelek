using App.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace App.Auth
{
    public class ApiClient
    {
        private readonly HttpClient _http;
        public ApiClient(string baseUrl)
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(baseUrl, UriKind.Absolute),
                Timeout = TimeSpan.FromSeconds(20)
            };
        }

        public async Task Login(string username, string password)
        {
            var request = new LoginRequest(username, password)
            {
                Username = username,
                Password = password
            };

            using var response = await _http.PostAsJsonAsync("auth/login", request);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("Invalid username or password.");

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<LoginResponse>() ?? throw new InvalidOperationException("Failed to parse login response.");

            Session.Set(data.AccessToken, data.ExpiresAt);
        }
    }
}
