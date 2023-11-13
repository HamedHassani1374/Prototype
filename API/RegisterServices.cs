using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Prototype.Persistance.JWTServices;
using System.Text;
using Prototype.Services.UserAuth;
using Prototype.Repository;
using AutoMapper;
using Prototype.Persistance.Mapping;

namespace Prototype.API
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

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.AllowNullDestinationValues = false;
            });

            var mapper = config.CreateMapper();

            services.AddSingleton(mapper);
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
