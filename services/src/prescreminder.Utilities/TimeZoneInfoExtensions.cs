using System;

namespace prescreminder.Utilities
{
    public static class TimeZoneInfoExtensions
    {
        public static DateTime GetStartOfDayUtc(this TimeZoneInfo timeZoneInfo)
        {
            var localTime = DateTime.UtcNow.Add(timeZoneInfo.BaseUtcOffset);
            var result = new DateTime(localTime.Year, localTime.Month, localTime.Day, 0, 0, 0, 0, DateTimeKind.Utc);
            return result.Subtract(timeZoneInfo.BaseUtcOffset);
        }
        public static DateTime GetEndOfDayUtc(this TimeZoneInfo timeZoneInfo)
        {
            var localTime = DateTime.UtcNow.Add(timeZoneInfo.BaseUtcOffset);
            var result = new DateTime(localTime.Year, localTime.Month, localTime.Day, 23, 59, 59, 999, DateTimeKind.Utc);
            return result.Subtract(timeZoneInfo.BaseUtcOffset);
        }
    }
}