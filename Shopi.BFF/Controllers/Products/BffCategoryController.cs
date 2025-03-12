using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.BFF.DTOs.Category;
using Shopi.Core.Interfaces;
using Shopi.Core.Utils;

namespace Shopi.BFF.Controllers.Products;

[ApiController]
[Route("/api/v1/category")]
public class BffCategoryController : ControllerBase
{
    private readonly IBffHttpClient _httpClient;

    public BffCategoryController(IBffHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListCategories([FromQuery] FilterCategoriesDto query)
    {
        var queryParams = query.ToDictionary();
        var categoryResponse =
            await _httpClient.Get<object>(MicroServicesUrls.CategoryApiUrl, "filter", queryParams: queryParams);
        if (!categoryResponse.IsSuccessStatusCode)
        {
            var errorContent = await categoryResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        await Task.Delay(500);
        
        var content = await categoryResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<FilterCategoryResponseDto>(content);
        return Ok(deserializedContent);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto dto)
    {
        var categoryResponse =
            await _httpClient.PostJsonAsync<object>(MicroServicesUrls.CategoryApiUrl, "create", data: dto);
        
        if (!categoryResponse.IsSuccessStatusCode)
        {
            var errorContent = await categoryResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }
        
        var content = await categoryResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<Category>(content);
        return Created(string.Empty, deserializedContent);
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        var categoryResponse =
            await _httpClient.PatchJsonAsync<object>(MicroServicesUrls.CategoryApiUrl, $"update/{id}", data: dto);
        
        if (!categoryResponse.IsSuccessStatusCode)
        {
            var errorContent = await categoryResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }
        
        var content = await categoryResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<Category>(content);
        return Ok(deserializedContent);
    }
}