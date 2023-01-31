using BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegistrationModel registrationModel);
        Task<string> LoginAsync(LoginModel loginModel);
        Task<IdentityUser?> GetCurrentUserAsync();
    }
}
