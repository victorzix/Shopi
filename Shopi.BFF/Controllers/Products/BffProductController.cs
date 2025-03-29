using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.BFF.DTOs.Product;
using Shopi.Core.Interfaces;
using Shopi.Core.Utils;

namespace Shopi.BFF.Controllers.Products;

[ApiController]
[Route("/api/v1/product")]
[Tags("Products")]
public class BffProductController : ControllerBase
{
    private readonly IBffHttpClient _httpClient;

    public BffProductController(IBffHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("list")]
    public async Task<IActionResult> ListProducts([FromQuery] FilterProductsDto query)
    {
        var queryParams = query.ToDictionary();
        var productResponse =
            await _httpClient.Get<object>(MicroServicesUrls.ProductApiUrl, "filter", queryParams: queryParams);
        if (!productResponse.IsSuccessStatusCode)
        {
            var errorContent = await productResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }
        
        await Task.Delay(1000);
        var content = await productResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<FilterProductResponseDto>(content);
        return Ok(deserializedContent);
    }
    
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var productResponse =
            await _httpClient.Get<object>(MicroServicesUrls.ProductApiUrl, $"get-product/{id}");
        if (!productResponse.IsSuccessStatusCode)
        {
            var errorContent = await productResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await productResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<Product>(content);
        return Ok(deserializedContent);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        var productResponse =
            await _httpClient.PostJsonAsync<object>(MicroServicesUrls.ProductApiUrl, "create", data: dto);

        if (!productResponse.IsSuccessStatusCode)
        {
            var errorContent = await productResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await productResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<Product>(content);
        return Created(string.Empty, deserializedContent);
    }

    [HttpPatch("update/{id}")]
    public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto dto)
    {
        var productResponse =
            await _httpClient.PatchJsonAsync<object>(MicroServicesUrls.ProductApiUrl, $"update/{id}", data: dto);

        if (!productResponse.IsSuccessStatusCode)
        {
            var errorContent = await productResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        await Task.Delay(1000);
        var content = await productResponse.Content.ReadAsStringAsync();
        var deserializedContent = JsonConvert.DeserializeObject<Product>(content);
        return Ok(deserializedContent);
    }

    [HttpPatch("change-visibility/{id}")]
    public async Task<IActionResult> ChangeProductVisibility(Guid id)
    {
        var productResponse =
            await _httpClient.PatchAsyncWithoutData(MicroServicesUrls.ProductApiUrl, $"change-visibility/{id}");

        if (!productResponse.IsSuccessStatusCode)
        {
            var errorContent = await productResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        await Task.Delay(1000);

        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        var productResponse =
            await _httpClient.Delete(MicroServicesUrls.ProductApiUrl, $"delete/{id}");

        if (!productResponse.IsSuccessStatusCode)
        {
            var errorContent = await productResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        return NoContent();
    }
}