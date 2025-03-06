using MediatR;
using Shopi.Core.Utils;
using Shopi.Images.API.Models;

namespace Shopi.Images.API.Queries;

public class GetImageQuery : IRequest<ApiResponses<Image>>
{
    public string Id { get; set; }

    public GetImageQuery()
    {
    }

    public GetImageQuery(string id)
    {
        Id = id;
    }
}