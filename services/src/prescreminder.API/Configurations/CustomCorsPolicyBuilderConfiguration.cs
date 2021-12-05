using Microsoft.AspNetCore.Cors.Infrastructure;

namespace prescreminder.API.Configurations
{
    public static class CustomCorsPolicyBuilderConfiguration
    {
        public static CorsPolicyBuilder WithExposedCustomHeaders(this CorsPolicyBuilder builder)
        {
            builder.WithExposedHeaders("Content-Disposition");
            return builder;
        }
    }
}