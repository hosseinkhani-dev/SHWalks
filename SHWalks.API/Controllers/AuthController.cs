using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHWalks.Application.AuthServices.RegisterServices;
using SHWalks.Application.AuthServices.RegisterServices.DTOs;
using SHWalks.Application.AuthServices.TokenServices;
using SHWalks.Application.AuthServices.TokenServices.DTOs;

namespace SHWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IRegisterService _registerService;

        public AuthController(
            ITokenService tokenService,
            IRegisterService registerService)
        {
            _tokenService = tokenService;
            _registerService = registerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if(await _registerService.RegisterAsync(dto))
            {
                return Ok("User was registerd");
            }

            return BadRequest("Somthing went wrong!");
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
