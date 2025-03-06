using MediatR;
using Shopi.Core.Utils;
using Shopi.Images.API.Interfaces;
using Shopi.Images.API.Models;
using Shopi.Images.API.Queries;

namespace Shopi.Images.API.QueryHandlers;

public class ListImagesQueryHandlers : IRequestHandler<ListImagesQuery, ApiResponses<IReadOnlyCollection<Image>>>
{
    private readonly IImageReadRepository _repository;

    public ListImagesQueryHandlers(IImageReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponses<IReadOnlyCollection<Image>>> Handle(ListImagesQuery request,
        CancellationToken cancellationToken)
    {
        var images = await _repository.ListImages(request);
        return new ApiResponses<IReadOnlyCollection<Image>>
        {
            Data = images,
            Success = true,
        };
    }
}