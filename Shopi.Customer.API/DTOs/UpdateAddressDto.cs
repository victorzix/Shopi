namespace Shopi.Customer.API.DTOs;

public class UpdateAddressDto
{
    public string? Title { get; set; }
    public string? Street { get; set; }
    public string? District { get; set; }
    public int? Number { get; set; }
    public string? ZipCode { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Complement { get; set; }
}