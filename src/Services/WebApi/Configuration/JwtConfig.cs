using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApi.Configuration
{
    public static class JwtConfig
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services, bool IsDevelopment, string publicKeyJWT)
        {
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });




            return services;
        }

        public static WebApplication UseAuthConfiguration(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
