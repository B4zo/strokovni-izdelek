using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Auth
{
    public class Session
    {
        public static string? AccessToken { get; private set; }
        public static DateTimeOffset? ExpiresAt { get; private set; }

        public static bool IsLoggedIn => !string.IsNullOrEmpty(AccessToken) && ExpiresAt is not null && ExpiresAt.Value > DateTimeOffset.UtcNow;

        public static void Set(string accessToken, DateTimeOffset expiresAt)
        {
            AccessToken = accessToken;
            ExpiresAt = expiresAt;
        }

        public static void Clear()
        {
            AccessToken = null;
            ExpiresAt = null;
        }

    }
}
