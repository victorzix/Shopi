using AutoMapper;
using MediatR;
using Shopi.Core.Utils;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;
using Shopi.Product.API.Queries;

namespace Shopi.Product.API.QueryHandlers;

public class
    FilterCategoriesQueryHandler : IRequestHandler<FilterCategoriesQuery, ApiResponses<IReadOnlyCollection<Category>>>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly IMapper _mapper;

    public FilterCategoriesQueryHandler(ICategoryReadRepository readRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<IReadOnlyCollection<Category>>> Handle(FilterCategoriesQuery request,
        CancellationToken cancellationToken)
    {
        var categories = await _readRepository.FilterCategories(request);

        return new ApiResponses<IReadOnlyCollection<Category>>
        {
            Data = categories,
            Success = true
        };
    }
}