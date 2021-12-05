using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace prescreminder.Utilities
{
    public static class HttpResponseExtensions
    {
        public static void SetFileName(this HttpResponse httpResponse, string fileName)
        {
            var cd = new ContentDisposition
            {
                FileName = fileName,
                Inline = false
            };
            httpResponse.Headers.Add("Content-Disposition", cd.ToString());
        }
    }
}
