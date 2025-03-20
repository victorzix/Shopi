using MediatR;
using Shopi.Core.Utils;
using Shopi.Images.API.Models;

namespace Shopi.Images.API.Queries;

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