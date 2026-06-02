using Microsoft.AspNetCore.Mvc;
using SHWalks.UI.Models;
using System.Text;
using System.Text.Json;

namespace SHWalks.UI.Controllers
{
    public class AreasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AreasController(
            IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var modelList = new List<AreaViewModel>();

            // Create the HTTP client
            var client = _httpClientFactory.CreateClient();

            // Call the API 
            var response = await client.GetAsync("https://localhost:2026/api/areas");

            // If successful, convert JSON string into the C# List
            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var options =
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                modelList =
                    JsonSerializer.Deserialize<List<AreaViewModel>>(
                        responseBody, options) ?? new List<AreaViewModel>();
            }

            return View(modelList);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var model = new AreaViewModel();

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:2026/api/areas/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStreamAsync();

                var options =
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                model = 
                    JsonSerializer.Deserialize<AreaViewModel>(responseBody, options) 
                    ?? new AreaViewModel();
            }

            return View(model);
        }


        // To add an area
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddAreaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var client = _httpClientFactory.CreateClient();

            var jsonBody = JsonSerializer.Serialize(viewModel);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:2026/api/areas", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "ServerError. Please try again later.");
            return View(viewModel);
        }
    }
}
