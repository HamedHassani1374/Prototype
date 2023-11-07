using AutoWrapper.Wrappers;
using Carter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs.Request.User;
using Services.UserAuth;

namespace API.APIs
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
