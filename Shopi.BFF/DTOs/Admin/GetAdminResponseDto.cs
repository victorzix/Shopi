namespace Shopi.BFF.DTOs.Admin;

public class GetAdminResponseDto
{
    public Guid UserId { get; set; } 
    public string Name { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
}