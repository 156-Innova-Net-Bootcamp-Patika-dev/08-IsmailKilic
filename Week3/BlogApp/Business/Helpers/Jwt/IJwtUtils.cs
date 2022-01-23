using System.IdentityModel.Tokens.Jwt;
using Entities.Concrete;

namespace Business.Helpers.Jwt
{
    public interface IJwtUtils
    {
        string Generate(User user);
        JwtSecurityToken ValidateToken(string token);
    }
}
