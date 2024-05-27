using Client.Layer.Data;
using Client.Layer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Client.Layer.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettingsJson = appSettingsSection.Get<AppSettings>();

            #region Identity Context
            services.AddDbContext<SecurityDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("Default")));
            #endregion

            #region Identity Middleware Configuration
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<SecurityDbContext>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMessagePtBr>();
            #endregion

            #region JWT Authentication Middleware Configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true; //facilitates the application validate the token after signin
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingsJson.Secret)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettingsJson.ValidAudience,
                    ValidIssuer = appSettingsJson.Host
                };
            });
            #endregion

            return services;
        }
    }
}
