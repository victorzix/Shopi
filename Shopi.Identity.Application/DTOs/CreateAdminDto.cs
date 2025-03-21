namespace Shopi.Identity.Application.DTOs;

public class CreateAdminDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}