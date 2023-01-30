using BLL.Models;

namespace BLL.Interfaces
{
    public interface IAuthService
    {
        Task<UserDto> RegisterAsync(RegistrationModel registerModel);
        Task<string> LoginAsync(LoginModel loginModel);
        string? GetCurrentUserId();
    }
}
