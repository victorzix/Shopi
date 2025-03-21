namespace Shopi.Customer.Domain.Entities;

public class Address
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public AppCustomer AppCustomer { get; set; }

    public string Title { get; set; }
    public string Street { get; set; }
    public string? District { get; set; }
    public int? Number { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string? Complement { get; set; }
    public bool IsMain { get; set; } = false;
}