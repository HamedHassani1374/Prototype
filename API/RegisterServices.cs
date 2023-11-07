using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistance.JWTServices;
using System.Text;
using Services.UserAuth;
using Repository;


namespace API
{
    public static class RegisterServicesExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            #region Services
            
            services.AddScoped<IJWTService, JWTService>();  
            services.AddScoped<IUserAuthService, UserAuthService>();
            services.AddScoped<UserAuthService>();

            #endregion

            #region Repo
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
            return services;
        }

        public static IServiceCollection JWTRegisterService(this IServiceCollection services, JwtSettings jwtSettings)
        {

            byte[]? key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        //ClockSkew = TimeSpan.Zero // for validate lifetime
                    };
                });
            services.AddAuthorization();

            return services;

        }
    }
}
