using Microsoft.AspNetCore.Mvc;

namespace Client.Layer.Configuration
{
    public static class ResponseBehaviorConfig
    {
        public static IServiceCollection ConfigureApiResponseBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
            return services;
        }
    }
}
