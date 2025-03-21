using MediatR;
using Shopi.Core.Utils;
using Shopi.Images.Domain.Entities;

namespace Shopi.Images.Application.Queries;

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