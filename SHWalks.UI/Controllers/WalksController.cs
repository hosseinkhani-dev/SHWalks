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
    public async Task<IActionResult> Index(string? searchQuery)
    {
        var viewModel = new List<GetAllWalkViewModel>();

        var client = _httpClientFactory.CreateClient();

        string url = "api/walks";

        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            url += $"?filterOn=Name&filterQuery={Uri.EscapeDataString(searchQuery)}";

            ViewData["CurrentFilter"] = searchQuery;
        }

        var response = await client.GetAsync(url);

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

        var response = await client.GetAsync($"api/walks/{id}");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

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

        var response = await client.GetAsync("api/areas");

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

        if (model.File != null && model.File.Length > 0)
        {
            using (var content = new MultipartFormDataContent())
            {
                using (var fileStream = model.File.OpenReadStream())
                {
                    var fileContent = new StreamContent(fileStream);
                    fileContent.Headers.ContentType =
                        new MediaTypeHeaderValue(model.File.ContentType);

                    content.Add(fileContent, "File", model.File.FileName);

                    var imageResponse = await client.PostAsync("api/images/upload", content);

                    if (imageResponse.IsSuccessStatusCode)
                    {
                        var imageResponseBody = await imageResponse.Content.ReadAsStreamAsync();

                        using (JsonDocument doc = JsonDocument.Parse(imageResponseBody))
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
        var response = await client.PostAsync("api/walks", jsonContent);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError("", "Server error occurred while saving the walk.");
        return View(model);
    }

    // Update A Walk
    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        var viewModel = new UpdateWalkViewModel();

        var client = _httpClientFactory.CreateClient();

        // Fetch Walk
        var walkResponse = await client.GetAsync($"api/walks/{id}");

        if (walkResponse.IsSuccessStatusCode)
        {
            var walkResponseBody = await walkResponse.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var walkDto = JsonSerializer.Deserialize<WalkDetailViewModel>(walkResponseBody, options);

            if (walkDto != null)
            {
                var diff = walkDto.Difficulty?.ToString().ToLower().Trim() ?? "";
                viewModel.Difficulties = new List<SelectListItem>
                {
                    new SelectListItem{ Value = "1", Text = "Easy", Selected = (diff == "1" || diff == "easy") },
                    new SelectListItem{ Value = "2", Text = "Medium", Selected = (diff == "2" || diff == "medium") },
                    new SelectListItem{ Value = "3", Text = "Hard", Selected = (diff == "3" || diff == "hard") },
                };

                // Fetch Areas
                var areaResponse = await client.GetAsync("api/areas");

                if (areaResponse.IsSuccessStatusCode)
                {
                    var areaResponseBody = await areaResponse.Content.ReadAsStringAsync();

                    var areas = JsonSerializer.Deserialize<List<AreaViewModel>>(areaResponseBody, options)
                        ?? new List<AreaViewModel>();

                    viewModel.Areas = areas.Select(area => new SelectListItem
                    {
                        Value = area.Id.ToString(),
                        Text = area.Name,
                        Selected = (area.Id == walkDto?.AreaDto.Id)
                    }).ToList();
                }

                viewModel.Id = walkDto.Id;
                viewModel.Name = walkDto.Name;
                viewModel.Description = walkDto.Description;
                viewModel.Length = walkDto.Length;
                viewModel.ExistingImageUrl = walkDto.ImageUrl;
                viewModel.SelectedDifficulty = walkDto.Difficulty;
                viewModel.SelectedAreaId = walkDto.AreaDto.Id;
            }
        }

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateWalkViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var client = _httpClientFactory.CreateClient();

        var uploadedImageUrl = model.ExistingImageUrl ?? "/image/Default-area-image-shiraz.jpg";

        if(model.File != null && model.File.Length > 0)
        {
            using(var content = new MultipartFormDataContent())
            {
                using(var fileStream = model.File.OpenReadStream())
                {
                    var fileContecnt = new StreamContent(fileStream);
                    fileContecnt.Headers.ContentType = new MediaTypeHeaderValue(model.File.ContentType);

                    content.Add(fileContecnt, "File", model.File.FileName);

                    var imageResponse = await client.PostAsync("api/images/upload", content);

                    if (imageResponse.IsSuccessStatusCode)
                    {
                        var imageResponseBody =
                            await imageResponse.Content.ReadAsStringAsync();
                        using (JsonDocument doc = JsonDocument.Parse(imageResponseBody))
                        {
                            uploadedImageUrl = doc.RootElement.GetProperty("imageUrl").GetString()
                                ?? uploadedImageUrl;
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Image upload faild. Please try again later");
                        return View(model);
                    }
                }
            }
        }

        var walkDto = new
        {
            Name = model.Name,
            Description = model.Description,
            Length = model.Length,
            ImageUrl = uploadedImageUrl,
            Difficulty = byte.Parse(model.SelectedDifficulty),
        };

        var jsonBody = JsonSerializer.Serialize(walkDto);
        var walkContent = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        var response = await client.PutAsync($"api/walks/{model.Id}", walkContent);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        ModelState.AddModelError(string.Empty, "Could not update the walk. Try again.");
        return View(model);
    }

    // Delete Walk
    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var viewModel = new WalkDetailViewModel();

        var client = _httpClientFactory.CreateClient();

        var response = await client.GetAsync($"https://localhost:2026/api/walks/{id}");

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            viewModel = JsonSerializer.Deserialize<WalkDetailViewModel>(responseBody, options)
                ?? new WalkDetailViewModel();
        }

        return View(viewModel);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var client = _httpClientFactory.CreateClient();

        var response = await client.DeleteAsync($"https://localhost:2026/api/walks/{id}");

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, "Could not delete the walk!");
        return RedirectToAction(nameof(Index));
    }
}
