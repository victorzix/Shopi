using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Images.API.Interfaces;
using Shopi.Images.API.Models;
using Shopi.Images.API.Queries;
using Shopi.Images.API.Validators;

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
        var validator = new ListImagesQueryValidator();
        var validate = await validator.ValidateAsync(request, cancellationToken);

        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validate.Errors.Select(e => e.ErrorMessage));
        }
        
        var images = await _repository.ListImages(request);
        return new ApiResponses<IReadOnlyCollection<Image>>
        {
            Data = images,
            Success = true,
        };
    }
}