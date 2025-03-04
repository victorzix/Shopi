using MediatR;
using Shopi.Admin.API.Models;
using Shopi.Core.Utils;

namespace Shopi.Admin.API.Queries;

public class FilterAdminQuery : IRequest<ApiResponses<AppAdmin>>
{
    public string? Email { get; set; }
    public Guid? Id { get; set; }

    public FilterAdminQuery() { }

    public FilterAdminQuery(string? email, Guid? id)
    {
        Email = email;
        Id = id;
    }
}