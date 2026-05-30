using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHWalks.Application.TokenServices;
using SHWalks.Application.TokenServices.DTOs;

namespace SHWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            if (dto.UserName == "admin" && dto.Password == "Password")
            {
                var token = _tokenService.CreateToken(dto.UserName, "Admin");

                return Ok(token);
            }

            return Unauthorized("Invalid username or password!");
        }
    }
}
