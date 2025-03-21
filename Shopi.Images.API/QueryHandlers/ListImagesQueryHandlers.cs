using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Images.Domain.Interfaces;
using Shopi.Images.Domain.Entities;
using Shopi.Images.Application.Queries;
using Shopi.Images.Application.Validators;
using Shopi.Images.Domain.Queries;

namespace Shopi.Images.API.QueryHandlers;

public class ListImagesQueryHandlers : IRequestHandler<ListImagesQuery, ApiResponses<IReadOnlyCollection<Image>>>
{
    private readonly IImageReadRepository _repository;
    private readonly IMapper _mapper;

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

        var imageQuery = _mapper.Map<QueryImages>(request);
        
        var images = await _repository.ListImages(imageQuery);
        return new ApiResponses<IReadOnlyCollection<Image>>
        {
            Data = images,
            Success = true,
        };
    }
}