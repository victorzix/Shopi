﻿using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.Application.Commands.ProductsCommands;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Application.Validators;
using Shopi.Product.Domain.Entities;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.CommandHandlers.ProductsCommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponses<ProductResponseDto>>
{
    private IProductWriteRepository _productWriteRepository;
    private IProductReadRepository _productReadRepository;
    private IMapper _mapper;

    public CreateProductCommandHandler(
        IProductWriteRepository productWriteRepository,
        IMapper mapper,
        IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _mapper = mapper;
        _productReadRepository = productReadRepository;
    }

    public async Task<ApiResponses<ProductResponseDto>> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validate = await validator.ValidateAsync(request, cancellationToken);
        if (!validate.IsValid)
        {
            throw new CustomApiException("Erro de validação", StatusCodes.Status400BadRequest,
                validate.Errors.Select(e => e.ErrorMessage));
        }

        if (!string.IsNullOrEmpty(request.Sku))
        {
            var checkSku = await _productReadRepository.GetBySku(request.Sku);
            if (checkSku != null)
            {
                throw new CustomApiException("Erro ao criar produto", StatusCodes.Status400BadRequest,
                    "SKU já cadastrado");
            }
        }

        var appProduct = await _productWriteRepository.Create(_mapper.Map<AppProduct>(request));

        return new ApiResponses<ProductResponseDto>
        {
            Data = _mapper.Map<ProductResponseDto>(appProduct),
            Success = true
        };
    }
}