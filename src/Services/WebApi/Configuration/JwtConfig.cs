using Core.Crypto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.Core.Identidade;

namespace WebApi.Configuration
{
    public static class JwtConfig
    {
        public static IServiceCollection AddAuthConfiguration(this IServiceCollection services
                                                             ,IConfiguration configuration
                                                             ,bool IsDevelopment
                                                             ,string publicKeyJWT)
        {
            var jwtOptions = configuration.GetSection("Jwt").Get<JwtBearerOptions>();

            services.AddTransient<IClaimsTransformation>(_ => new ClaimsTransformer(jwtOptions.Audience));

            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            authenticationBuilder.AddJwtBearer(options =>
            {
                options.Audience = jwtOptions.Audience;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidIssuers = new[] { "http://localhost:8080/realms/react-app-realm" },
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = RSAKeyGenerator.BuildRSAKey(publicKeyJWT),
                    ValidateLifetime = true
                };

                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = tokenContext =>
                    {
                        Console.WriteLine("User successfully authenticated");
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = tokenContext =>
                    {
                        tokenContext.NoResult();
                        tokenContext.Response.StatusCode = 500;
                        tokenContext.Response.ContentType = "text/plain";
                        if (IsDevelopment)
                        {
                            return tokenContext.Response.WriteAsync(tokenContext.Exception.ToString());
                        }
                        return tokenContext.Response.WriteAsync("An error occured processing your authentication.");
                    }
                };
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
