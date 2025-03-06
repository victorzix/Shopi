﻿using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Images.API.Interfaces;
using Shopi.Images.API.Models;
using Shopi.Images.API.Queries;
using Shopi.Images.API.Validators;

namespace Shopi.Images.API.QueryHandlers;

public class GetImageQueryHandler : IRequestHandler<GetImageQuery, ApiResponses<Image>>
{
    private readonly IImageReadRepository _repository;

    public GetImageQueryHandler(IImageReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponses<Image>> Handle(GetImageQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetImageQueryValidator();
        var validate = await validator.ValidateAsync(request, cancellationToken);

        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest, validate.Errors.Select(e => e.ErrorMessage));
        }
        
        var image = await _repository.GetImage(request.Id);
        if (image == null)
        {
            throw new CustomApiException("Erro ao localizar imagem", StatusCodes.Status404NotFound,
                "Imagem não encontrada");
        }

        return new ApiResponses<Image>
        {
            Data = image,
            Success = true,
        };
    }
}