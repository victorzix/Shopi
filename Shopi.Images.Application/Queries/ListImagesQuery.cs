using MediatR;
using Shopi.Core.Utils;
using Shopi.Images.Domain.Entities;

namespace Shopi.Images.Application.Queries;

public class ListImagesQuery : IRequest<ApiResponses<IReadOnlyCollection<Image>>>
{
    public Guid ProductId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int Limit { get; set; } = 3;

    public ListImagesQuery()
    {
    }

    public ListImagesQuery(Guid productId, int pageNumber, int limit)
    {
        ProductId = productId;
        PageNumber = pageNumber;
        Limit = limit;
    }
}