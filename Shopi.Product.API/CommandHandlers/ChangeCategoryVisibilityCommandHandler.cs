using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Core.Utils;
using Shopi.Product.Application.Commands;
using Shopi.Product.Application.DTOs;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.CommandHandlers;

public class
    ChangeCategoryVisibilityCommandHandler : IRequestHandler<ChangeVisibilityCommand,
    ApiResponses<CreateCategoryResponseDto>>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public ChangeCategoryVisibilityCommandHandler(ICategoryReadRepository readRepository,
        ICategoryWriteRepository writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponses<CreateCategoryResponseDto>> Handle(ChangeVisibilityCommand request,
        CancellationToken cancellationToken)
    {
        var category = await _readRepository.Get(request.Id);
        if (category == null)
        {
            throw new CustomApiException("Erro ao atualizar categoria", StatusCodes.Status400BadRequest,
                "Categoria não encontrada");
        }

        await _writeRepository.ChangeVisibility(category);

        category.Visible = !category.Visible;
        return new ApiResponses<CreateCategoryResponseDto>
        {
            Data = _mapper.Map<CreateCategoryResponseDto>(category),
            Success = true
        };
    }
}