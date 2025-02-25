using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Queries;

public class FilterCustomerQuery : IRequest<ApiResponses<AppCustomer>>
{
    public string? Email { get; set; }
    public string? Document { get; set; }
    public Guid? Id { get; set; }

    public FilterCustomerQuery() { }

    public FilterCustomerQuery(string? email, string? document, Guid? id)
    {
        Email = email;
        Document = document;
        Id = id;
    }
}