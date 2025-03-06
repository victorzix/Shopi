using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.API.Commands;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;
using Shopi.Product.API.Validators;

namespace Shopi.Product.API.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponses<CreateProductResponseDto>>
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

    public async Task<ApiResponses<CreateProductResponseDto>> Handle(CreateProductCommand request,
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

        return new ApiResponses<CreateProductResponseDto>
        {
            Data = _mapper.Map<CreateProductResponseDto>(appProduct),
            Success = true
        };
    }
}