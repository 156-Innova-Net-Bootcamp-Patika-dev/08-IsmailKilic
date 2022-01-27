using System.IdentityModel.Tokens.Jwt;
using Entities.Concrete;

namespace Business.Helpers.Jwt
{
    /// <summary>
    /// Interface for jwt operations
    /// </summary>
    public interface IJwtUtils
    {
        string Generate(User user);
        JwtSecurityToken ValidateToken(string token);
    }
}
