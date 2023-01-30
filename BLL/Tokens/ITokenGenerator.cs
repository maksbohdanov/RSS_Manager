using Microsoft.AspNetCore.Identity;

namespace BLL.Tokens
{
    public interface ITokenGenerator
    {
        string GenerateToken(IdentityUser user);
    }
}
