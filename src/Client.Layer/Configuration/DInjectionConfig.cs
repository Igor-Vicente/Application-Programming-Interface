using Business.Layer.Interfaces;
using Business.Layer.Notifications;
using Business.Layer.Services;
using Client.Layer.Extensions;
using Data.Layer.Context;
using Data.Layer.Repository;

namespace Client.Layer.Configuration
{
    public static class DInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<INotificator, Notificator>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();
            return services;
        }
    }
}
