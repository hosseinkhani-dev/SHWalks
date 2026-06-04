using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SHWalks.UI.Models.Areas;
using SHWalks.UI.Models.Walks;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;

namespace SHWalks.UI.Controllers;

public class WalksController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WalksController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Get All
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var viewModel = new List<GetAllWalkViewModel>();

        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync("https://localhost:2026/api/walks");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            viewModel = JsonSerializer.Deserialize<List<GetAllWalkViewModel>>(responseBody, options);
        }
        return View(viewModel);
    }

    // Get By Id
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var viewModel = new WalkDetailViewModel();

        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync($"https://localhost:2026/api/walks/{id}");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true };

            viewModel = JsonSerializer.Deserialize<WalkDetailViewModel>(responseBody, options);
        }

        return View(viewModel);
    }

    //Add Walk
    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var viewModel = new AddWalkViewModel()
        {
            Difficulties = new List<SelectListItem>
            {
                new SelectListItem{Value = "1", Text = "Easy"},
                new SelectListItem{Value = "2", Text = "Medium"},
                new SelectListItem{Value = "3", Text = "Hard"},
            }
        };

        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync("https://localhost:2026/api/areas");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var areas = JsonSerializer.Deserialize<List<AreaViewModel>>(responseBody, options)
                ?? new List<AreaViewModel>();

            viewModel.Areas = areas.Select(area => new SelectListItem
            {
                Value = area.Id.ToString(),
                Text = area.Name,
            }).ToList();
        }

        return View(viewModel);
    }


    [HttpPost]
    public async Task<IActionResult> Add(AddWalkViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = _httpClientFactory.CreateClient();

        // Upload Image
        string uploadedImageUrl = "/images/Default-area-image-shiraz.jpg";

        if(model.File != null && model.File.Length > 0)
        {
            using(var content = new MultipartFormDataContent())
            {
                using(var fileStream = model.File.OpenReadStream())
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(model.File.ContentType);
                    
                    content.Add(fileContent, "File", model.File.FileName);

                    var imageResponse = await client.PostAsync("https://localhost:2026/api/images/upload", content);

                    if (imageResponse.IsSuccessStatusCode)
                    {
                        var imageResponseBody = await imageResponse.Content.ReadAsStreamAsync();

                        using(JsonDocument doc = JsonDocument.Parse(imageResponseBody))
                        {
                            uploadedImageUrl = doc.RootElement.GetProperty("imageUrl").GetString()
                                ?? uploadedImageUrl;
                        }
                    }
                }
            }
        }

        var apiPayload = new
        {
            Name = model.Name,
            Description = model.Description,
            Length = model.Length,
            ImageUrl = uploadedImageUrl,
            Difficulty = byte.Parse(model.SelectedDifficulty),
            AreaId = model.SelectedAreaId
        };

        var jsonContent = new StringContent(JsonSerializer.Serialize(apiPayload), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:2026/api/walks", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Server error occurred while saving the walk.");
        return View(model);
    }

}
