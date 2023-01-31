using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using BLL.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<IdentityUser> userManager, ITokenGenerator tokenGenerator,
            IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _userManager= userManager;
            _mapper= mapper;
            _tokenGenerator= tokenGenerator;
            _httpContextAccessor= httpContextAccessor;
        }       

        public async Task<UserDto> RegisterAsync(RegistrationModel registrationModel)
        {
            var user = _mapper.Map<IdentityUser>(registrationModel);
            var result = await _userManager.CreateAsync(user, registrationModel.Password);

            if (!result.Succeeded)
            {
                var message = string.Join(" ", result.Errors.Select(e => e.Description));
                throw new RssManagerException(message);
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
                throw new NotFoundException("User with specified email was not found");

            var passwordCorrect = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!passwordCorrect)
                throw new RssManagerException("Wrong password.");

            return _tokenGenerator.GenerateToken(user);
        }

        public async Task<IdentityUser?> GetCurrentUserAsync()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            return await _userManager.GetUserAsync(principal);            
        }
    }
}
