using Inventory_Management.Common.Exceptions;
using Inventory_Management.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Inventory_Management.Common.Helpers
{
    public static class TokenGenerator
    {
        public static async Task<ResultDto<string>> GenerateToken(User user, IConfiguration config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.GetSection("JwtSettings:SECRET_KEY").Value);
            try
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                             {
                    //new Claim("Id", user.ID.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("RoleID", user.RoleID.ToString()),
                    new Claim("Email", user.Email)
                             }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    Issuer = Environment.GetEnvironmentVariable("ISSUER"),
                    Audience = Environment.GetEnvironmentVariable("AUDIENCE"),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return ResultDto<string>.Sucess(tokenHandler.WriteToken(token), "");

            }
            catch (Exception ex)
            {
                return ResultDto<string>.Faliure(ErrorCode.UnableTogenerateToken, "Unable to Create Token");
            }
        }
    }
}
