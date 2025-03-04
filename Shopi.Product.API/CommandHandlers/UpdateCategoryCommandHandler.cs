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
    UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponses<CreateCategoryResponseDto>>
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

    public async Task<ApiResponses<CreateCategoryResponseDto>> Handle(UpdateCategoryCommand request,
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

        return new ApiResponses<CreateCategoryResponseDto>
        {
            Data = _mapper.Map<CreateCategoryResponseDto>(category),
            Success = true,
        };
    }
}