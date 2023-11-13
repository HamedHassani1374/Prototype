using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prototype.Model.DTOs.Request.User;
using Prototype.Services.UserAuth;

namespace Prototype.API.APIs
{
    public static class UserAuth
    {
        public static void AddUserAuthEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/Login", [AllowAnonymous] async ([FromBody] UserLoginRequestDTO request) =>
            {
                using (var scope = app.ServiceProvider.CreateScope())
                {
                    var UserAuthService = scope.ServiceProvider.GetRequiredService<UserAuthService>();

                    var response = UserAuthService.Login(request).GetAwaiter().GetResult();

                    if (response.Success)
                    {
                        return new ApiResponse(response);
                    }
                    else
                        return new ApiResponse(response, response.StatusCode);
                }

            });
        }
    }
}
