namespace WebApi.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("All", configurePolicy =>
                {
                    configurePolicy
                         .AllowAnyMethod()
                         .AllowAnyHeader()
                         .AllowAnyOrigin();
                });
            });

            return services;
        }

        public static WebApplication UseCorsConfiguration(this WebApplication app)
        {
            app.UseCors("All");

            return app;
        }
    }
}
