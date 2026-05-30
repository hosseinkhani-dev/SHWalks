using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SHWalks.Application.AuthServices.RegisterServices;
using SHWalks.Application.AuthServices.RegisterServices.DTOs;
using SHWalks.Application.AuthServices.TokenServices;
using SHWalks.Application.AuthServices.TokenServices.DTOs;
using System.Threading.Tasks;

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
            if (await _registerService.RegisterAsync(dto))
            {
                return Ok("User was registerd");
            }

            return BadRequest("Somthing went wrong!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {

            var token = await _tokenService.CreateTokenAsync(dto);

            if(token == null)
            return Unauthorized("Invalid username or password!");

            return Ok(token);
        }
    }
}
