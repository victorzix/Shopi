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

namespace Shopi.Product.API.CommandHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponses<CreateProductResponseDto>>
{
    private IProductWriteRepository _productWriteRepository;
    private IProductReadRepository _productReadRepository;
    private IProductCategoryWriteRepository _productCategoryWriteRepository;
    private ICategoryReadRepository _categoryReadRepository;
    private IMapper _mapper;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository,
        IProductCategoryWriteRepository productCategoryWriteRepository, IMapper mapper,
        ICategoryReadRepository categoryReadRepository, IProductReadRepository productReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productCategoryWriteRepository = productCategoryWriteRepository;
        _mapper = mapper;
        _categoryReadRepository = categoryReadRepository;
        _productReadRepository = productReadRepository;
    }

    public async Task<ApiResponses<CreateProductResponseDto>> Handle(CreateProductCommand request,
        CancellationToken cancellationToken)
    {
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
        if (request.CategoriesIds != null && request.CategoriesIds.Count != 0)
        {
            var categories = await CheckCategories(request.CategoriesIds);
            await _productCategoryWriteRepository.AssociateCategoryToProduct(appProduct, categories);
        }
        
        return new ApiResponses<CreateProductResponseDto>
        {
            Data = _mapper.Map<CreateProductResponseDto>(appProduct),
            Success = true
        };
    }

    private async Task<List<Category>> CheckCategories(List<Guid> categoriesIds)
    {
        var categories = await _categoryReadRepository.GetMany(categoriesIds);

        if (categories.Count != categoriesIds.Count)
            throw new CustomApiException("Erro ao criar produto", StatusCodes.Status400BadRequest,
                "Alguma categoria não foi encontrada.");

        return categories;
    }
}