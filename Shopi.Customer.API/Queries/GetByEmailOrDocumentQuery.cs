using MediatR;
using Shopi.Core.Utils;
using Shopi.Customer.API.Models;

namespace Shopi.Customer.API.Queries;

public class GetByEmailOrDocumentQuery : IRequest<ApiResponses<AppCustomer>>
{
    public string? Email { get; set; }
    public string? Document { get; set; }

    public GetByEmailOrDocumentQuery() { }
    
    public GetByEmailOrDocumentQuery(string? email, string? document)
    {
        Email = email;
        Document = document;
    }
}