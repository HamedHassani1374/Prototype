using Microsoft.Extensions.Logging;
using Model.DTOs.Request;
using Model.DTOs.Request.User;
using Model.DTOs.Response;
using Model.DTOs.Response.UserAuth;
using Persistance.JWTServices;

namespace Services.UserAuth
{
    public class UserAuthService : IUserAuthService
    {
        private readonly IJWTService _jWTService;
        private readonly ILogger<UserAuthService> _logger;
        public UserAuthService(IJWTService jWTService , ILogger<UserAuthService> logger)
        {
            _jWTService = jWTService;
            _logger = logger;
        }

        public async Task<BaseResponse<UserLoginResponseDTO>> Login(UserLoginRequestDTO request)
        {
            var response = new BaseResponse<UserLoginResponseDTO>();

            try
            {
                if (request.UserName == "admin" && request.Password == "Password123")
                {
                    var userClaim = new UserClaimsRequestDTO
                    {
                        FullName = "admin",
                        Mobile = "09330488385",
                        UserID = 1,
                        UserName = "admin"
                    };

                    response.Response = new UserLoginResponseDTO
                    {
                        Token = _jWTService.GenerateToken(userClaim)
                    };

                    _logger.LogInformation($"User {request.UserName} has been logged in .");
                }
                else
                {
                    _logger.LogError($"User {request.UserName} Try to log in .");
                    response.Message = "User Not Found";
                    response.StatusCode = 400;
                }
            }


            catch (Exception ex)
            {
                _logger.LogInformation(ex, ex.Message);
                response.Message = ex.Message;
                response.StatusCode = 500;
            }
            return response;
        }
    }
}
