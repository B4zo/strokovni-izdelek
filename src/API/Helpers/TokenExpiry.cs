using TimeZoneConverter;

namespace API.Helpers
{
    public class TokenExpiry
    {
        public static DateTimeOffset NextMidnightLjubljanaUtc()
        {
            var tz = TZConvert.GetTimeZoneInfo("Europe/Ljubljana");

            var nowUtc = DateTimeOffset.UtcNow;
            var nowLocal = TimeZoneInfo.ConvertTime(nowUtc, tz);

            var nextMidnightLocal = new DateTimeOffset(
                nowLocal.Year, nowLocal.Month, nowLocal.Day,
                0, 0, 0,
                nowLocal.Offset
            ).AddDays(1);

            return TimeZoneInfo.ConvertTime(nextMidnightLocal, TimeZoneInfo.Utc);
        }
    }
}
