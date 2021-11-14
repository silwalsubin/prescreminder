using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;

namespace middleware.Authentication
{
    public static class HttpContextGetClaimTypeConfiguration
    {
        public static T GetClaimValue<T>(this HttpContext httpContext, string claimType)
        {
            var value = httpContext.User.Claims.Single(x => x.Type == claimType).Value;
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(value));
        }
    }
}