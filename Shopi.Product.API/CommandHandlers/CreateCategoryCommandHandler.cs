using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.API.Commands;
using Shopi.Product.API.DTOs;
using Shopi.Product.API.Interfaces;
using Shopi.Product.API.Models;

namespace Shopi.Product.API.CommandHandlers;

public class
    CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponses<CreateCategoryResponseDto>>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryReadRepository readRepository,
        ICategoryWriteRepository writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CreateCategoryResponseDto>> Handle(CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        if (request.ParentId.HasValue)
        {
            var parentCategory = await _readRepository.Get(request.ParentId.Value);

            if (parentCategory == null)
            {
                throw new CustomApiException("Erro ao criar subcategoria", StatusCodes.Status400BadRequest,
                    "Categoria pai não encontrada");
            }
        }

        var category = await _writeRepository.Create(_mapper.Map<Category>(request));

        return new ApiResponses<CreateCategoryResponseDto>
        {
            Data = _mapper.Map<CreateCategoryResponseDto>(category),
            Success = true,
        };
    }
}