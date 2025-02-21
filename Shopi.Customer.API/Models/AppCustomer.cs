namespace Shopi.Customer.API.Models;

public class AppCustomer
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public List<Address>? Addresses { get; set; }
}