using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shopi.BFF.DTOs.User;
using Shopi.Core.Services;
using Shopi.Core.Utils;

namespace Shopi.BFF.Controllers.Users;

[ApiController]
[Route("/api/v1/auth")]
[Tags("Authentication")]
public class BffAuthController : ControllerBase
{
    private readonly BffHttpClient _httpClient;

    public BffAuthController(BffHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("login-admin")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginUserDto dto)
    {
        var userResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.AuthApiUrl, "login-admin", dto);
        if (!userResponse.IsSuccessStatusCode)
        {
            var errorContent = await userResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await userResponse.Content.ReadAsStringAsync();
        return Ok(new { Token = content });
    }

    [HttpPost("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        var userResponse = await _httpClient.PostJsonAsyncWithoutData(MicroServicesUrls.AuthApiUrl, "confirm-email",
            new Dictionary<string, string> { { "token", token } });
        if (!userResponse.IsSuccessStatusCode)
        {
            var errorContent = await userResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await userResponse.Content.ReadAsStringAsync();
        return Ok(new { Token = content });
    }
    
    [HttpPost("resend-confirmation-email")]
    public async Task<IActionResult> ResendConfirmationEmail([FromQuery] string token)
    {
        var userResponse = await _httpClient.PostJsonAsyncWithoutData(MicroServicesUrls.AuthApiUrl, "resend-confirmation-email",
            new Dictionary<string, string> { { "token", token } });
        if (!userResponse.IsSuccessStatusCode)
        {
            var errorContent = await userResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        return NoContent();
    }


    [HttpPost("login-customer")]
    public async Task<IActionResult> LoginCustomer([FromBody] LoginUserDto dto)
    {
        var userResponse = await _httpClient.PostJsonAsync(MicroServicesUrls.AuthApiUrl, "login-customer", dto);
        if (!userResponse.IsSuccessStatusCode)
        {
            var errorContent = await userResponse.Content.ReadAsStringAsync();
            var deserializedErrorContent = JsonConvert.DeserializeObject<ErrorModel>(errorContent);
            return StatusCode(deserializedErrorContent.Status, deserializedErrorContent);
        }

        var content = await userResponse.Content.ReadAsStringAsync();
        return Ok(new { Token = content });
    }
}