namespace Client.Layer.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Development", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Production", builder =>
                builder
                .AllowAnyMethod()
                .WithOrigins("https://exemplo.com/")
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .AllowAnyHeader());

                /*
                 * .WithMethods("GET","POST")
                 * .WithHeaders(HeaderNames.ContentType, "application/json")
                */
            });
            return services;
        }
    }
}
