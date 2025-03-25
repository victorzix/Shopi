using AutoMapper;
using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Product.Application.Commands.CategoriesCommands;
using Shopi.Product.Domain.Interfaces;

namespace Shopi.Product.API.CommandHandlers.CategoriesCommandHandllers;

public class
    ChangeCategoryVisibilityCommandHandler : IRequestHandler<ChangeCategoryVisibilityCommand>
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;

    public ChangeCategoryVisibilityCommandHandler(ICategoryReadRepository readRepository,
        ICategoryWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(ChangeCategoryVisibilityCommand request,
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
    }
}