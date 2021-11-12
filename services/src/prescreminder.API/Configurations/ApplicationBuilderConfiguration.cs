using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace prescreminder.API.Configurations
{
    public class ApplicationBuilderConfiguration
    {
        public static void Configure(WebHostBuilderContext context, IApplicationBuilder app)
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseSpaStaticFiles();
                app.Map(new PathString("/view"), c =>
                {
                    c.UseSpa(spa =>
                    {
                        spa.Options.SourcePath = "Index.html";
                    });
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
