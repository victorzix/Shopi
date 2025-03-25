using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.Application.Commands.CategoriesCommands;
using Shopi.Product.Application.DTOs.Responses;
using Shopi.Product.Domain.Interfaces;


namespace Shopi.Product.API.CommandHandlers.CategoriesCommandHandllers;

public class
    UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponses<CategoryResponseDto>>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryReadRepository readRepository,
        ICategoryWriteRepository writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CategoryResponseDto>> Handle(UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var categoryToUpdate = await _readRepository.Get(request.Id);

        if (categoryToUpdate == null)
        {
            throw new CustomApiException("Erro ao atualizar categoria", StatusCodes.Status400BadRequest,
                "Categoria não encontrada");
        }

        if (request.ParentId.HasValue)
        {
            var parentCategory = await _readRepository.Get(request.ParentId.Value);

            if (parentCategory == null)
            {
                throw new CustomApiException("Erro ao atualizar categoria", StatusCodes.Status400BadRequest,
                    "Categoria pai não encontrada");
            }
        }

        var category = await _writeRepository.Update(_mapper.Map(request, categoryToUpdate));

        return new ApiResponses<CategoryResponseDto>
        {
            Data = _mapper.Map<CategoryResponseDto>(category),
            Success = true,
        };
    }
}