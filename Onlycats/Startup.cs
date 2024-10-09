

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOcelot();
    }

    public async Task Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        await app.UseOcelot();
    }
}