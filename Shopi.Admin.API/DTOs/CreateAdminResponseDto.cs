namespace Shopi.Admin.API.DTOs;

public class CreateAdminResponseDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
}