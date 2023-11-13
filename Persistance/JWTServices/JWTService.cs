using System.Text;
using Microsoft.Extensions.Options;
using Prototype.Model.DTOs.Request.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Prototype.Persistance.JWTServices
{
    public class JWTService : IJWTService
    {
        private readonly JwtSettings _options;
        public JWTService(IOptions<JwtSettings> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(UserClaimsRequestDTO user)
        {

            var secretKey = Encoding.UTF8.GetBytes(_options.SecretKey);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

            IEnumerable<Claim> claims = new List<Claim>
            {
                new Claim("FullName", user.FullName),
                new Claim("UserName", user.UserName),
                new Claim("Mobile", user.Mobile),
                new Claim("UserID", user.UserID.ToString()),
            };

            var descriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddMinutes(_options.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(claims),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(descriptor);

            var jwt = tokenHandler.WriteToken(securityToken);

            return jwt;


        }
        public ClaimsPrincipal? GetUserFromToken(string token)
        {
            try
            {
                var secretKey = Encoding.UTF8.GetBytes(_options.SecretKey);
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
                var jwtSecurityToken = securityToken as JwtSecurityToken;
                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");
                return principal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
