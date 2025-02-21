namespace Shopi.Identity.API.DTOs;

public class CreateCustomerDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Document { get; set; }
}