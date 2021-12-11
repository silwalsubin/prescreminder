using Microsoft.AspNetCore.Http;

namespace prescreminder.Utilities
{
    public static class HttpRequestExtensions
    {
        public static string GetUserTimeZone(this HttpRequest request)
        {
            var value = request.Headers["timeZone"].ToString();
            return value;
        }
    }
}