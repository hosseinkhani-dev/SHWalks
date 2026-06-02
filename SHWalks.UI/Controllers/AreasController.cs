using Microsoft.AspNetCore.Mvc;
using SHWalks.UI.Models;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

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

        //My index page
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

        // Details
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


        // Add an area
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

            // Upload Image **********
            string uploadedImageUrl = "/images/Default-area-image-shiraz.jpg";

            if(viewModel.File != null && viewModel.File.Length > 0)
            {
                using (var content = new MultipartFormDataContent())
                {
                    using(var fileStream = viewModel.File.OpenReadStream())
                    {
                        var fileContent = new StreamContent(fileStream);
                        fileContent.Headers.ContentType = 
                            new MediaTypeHeaderValue(viewModel.File.ContentType);
                        content.Add(fileContent, "File", viewModel.File.FileName);

                        var imageResponse = 
                            await client.PostAsync("https://localhost:2026/api/images/upload", content);

                        if (imageResponse.IsSuccessStatusCode)
                        {
                            var imageResponseBody = await imageResponse.Content.ReadAsStringAsync();
                            using (JsonDocument doc = JsonDocument.Parse(imageResponseBody))
                            {
                                uploadedImageUrl = doc.RootElement.GetProperty("imageUrl").GetString() 
                                    ?? uploadedImageUrl;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Image upload failed. Please try again or leave blank.");
                            return View(viewModel);
                        }
                    }
                }
            }

            var areaDto = new
            {
                Name = viewModel.Name,
                ImageUrl = uploadedImageUrl
            };

            var jsonBody = JsonSerializer.Serialize(areaDto);
            var areaContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:2026/api/areas", areaContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "ServerError. Please try again later.");
            return View(viewModel);
        }

        // Edit an area
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var viewModel = new UpdateAreaViewModel();

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:2026/api/areas/{id}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var options = 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var areaDto =  JsonSerializer.Deserialize<AreaViewModel>(responseBody, options);

                if(areaDto != null)
                {
                    viewModel.Id = areaDto.Id;
                    viewModel.Name = areaDto.Name;
                    viewModel.ExistingImageUrl = areaDto.ImageUrl;
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateAreaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var client = _httpClientFactory.CreateClient();

            //Upload Image
            var uploadedImageUrl = viewModel.ExistingImageUrl ?? "/image/Default-area-image-shiraz.jpg";

            if(viewModel.File != null && viewModel.File.Length > 0)
            {
                using(var content = new MultipartFormDataContent())
                {
                    using(var fileStream = viewModel.File.OpenReadStream())
                    {
                        var fileContent = new StreamContent(fileStream);
                        fileContent.Headers.ContentType =
                            new MediaTypeHeaderValue(viewModel.File.ContentType);

                        content.Add(fileContent, "File", viewModel.File.FileName);

                        var imageResponse =
                            await client.PostAsync(
                                "https://localhost:2026/api/images/upload", content);

                        if (imageResponse.IsSuccessStatusCode)
                        {
                            var imageResponseBody = 
                                await imageResponse.Content.ReadAsStringAsync();
                            using(JsonDocument doc = JsonDocument.Parse(imageResponseBody))
                            {
                                uploadedImageUrl = doc.RootElement.GetProperty("imageUrl").GetString()
                                    ?? uploadedImageUrl;
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Image upload faild. Please try again later");
                            return View(viewModel);
                        }
                    }
                }
            }

            var areaDto = new
            {
                Name = viewModel.Name,
                ImageUrl = uploadedImageUrl
            };

            var jsonBody = JsonSerializer.Serialize(areaDto);
            var areaContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = 
                await client.PutAsync(
                    $"https://localhost:2026/api/areas/{viewModel.Id}", areaContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id = viewModel.Id});
            }

            ModelState.AddModelError(string.Empty, "Could not update the area. Try again.");
            return View(viewModel);
        }

        // Delete an area
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var viewModel = new AreaViewModel();

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"https://localhost:2026/api/areas/{id}");

            if(response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                viewModel = JsonSerializer.Deserialize<AreaViewModel>(responseBody, options)
                    ?? new AreaViewModel();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync($"https://localhost:2026/api/areas/{id}");

            if(response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError(string.Empty, "Could not delete the area!");
            return RedirectToAction(nameof(Index));
        }

    }
}
