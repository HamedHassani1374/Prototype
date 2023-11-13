using Prototype.Model.DTOs.Request.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Persistance.JWTServices
{
    public interface IJWTService
    {
        string GenerateToken(UserClaimsRequestDTO user);
        ClaimsPrincipal? GetUserFromToken(string token);
    }
}
