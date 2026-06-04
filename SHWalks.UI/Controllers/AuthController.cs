using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SHWalks.UI.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SHWalks.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _httpClientFactory.CreateClient();

            var apiPayload = new
            {
                UserName = model.UserName,
                Password = model.Password,
                Roles = new string[] { "Reader", "Writer" }
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(apiPayload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/register", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Login));
            }

            var errorContent = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, !string.IsNullOrEmpty(errorContent) ? errorContent : "Registration failed.");

            return View(model);
        }

        // GET: Auth/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        // POST: Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = _httpClientFactory.CreateClient();

            var apiPayload = new
            {
                UserName = model.UserName,
                Password = model.Password
            };

            var jsonContent = new StringContent(JsonSerializer.Serialize(apiPayload), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/auth/login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var jwtTokenString = await response.Content.ReadAsStringAsync();

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(jwtTokenString) as JwtSecurityToken;

                if (jsonToken != null)
                {
                    var claims = jsonToken.Claims.ToList();

                    claims.Add(new Claim("JwtToken", jwtTokenString));

                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync("CookieAuth", principal);

                    return RedirectToAction("Index", "Walks");
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(model);
        }

        // POST: Auth/Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Clear the authentication cookie completely
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Home");
        }
    }
}
