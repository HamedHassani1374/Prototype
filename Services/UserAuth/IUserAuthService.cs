using Model.DTOs.Request;
using Model.DTOs.Request.User;
using Model.DTOs.Response;
using Model.DTOs.Response.UserAuth;

namespace Services.UserAuth
{
    public interface IUserAuthService
    {
        Task<BaseResponse<UserLoginResponseDTO>> Login(UserLoginRequestDTO request);
    }
}
