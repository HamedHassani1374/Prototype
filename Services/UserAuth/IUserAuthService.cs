using Prototype.Model.DTOs.Request.User;
using Prototype.Model.DTOs.Response;
using Prototype.Model.DTOs.Response.UserAuth;

namespace Prototype.Services.UserAuth
{
    public interface IUserAuthService
    {
        Task<BaseResponse<UserLoginResponseDTO>> Login(UserLoginRequestDTO request);
    }
}
