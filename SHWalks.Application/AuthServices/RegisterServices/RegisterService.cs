using Microsoft.AspNetCore.Identity;
using SHWalks.Application.AuthServices.RegisterServices.DTOs;

namespace SHWalks.Application.AuthServices.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.UserName,
            };

            var identityResult = await _userManager.CreateAsync(identityUser, dto.Password);

            if(identityResult.Succeeded)
            {
                if(dto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser, dto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
