

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

public class Startup
{
    private readonly IConfiguration _config;

    public Startup(IConfiguration conf)
    {
        _config = conf;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddOcelot();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["Jwt:Issuer"], //Replace the placeholders Jwt:Issuer, Jwt:Audience, and Jwt:Key with your actual JWT configuration values.
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]))

            };
        });
    }

    public async Task Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        await app.UseOcelot();
    }
}