using MediatR;
using Shopi.Core.Exceptions;
using Shopi.Product.Application.Commands.CategoriesCommands;
using Shopi.Product.Domain.Interfaces;
using Shopi.Product.Domain.Queries;

namespace Shopi.Product.API.CommandHandlers.CategoriesCommandHandllers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private ICategoryReadRepository _readRepository;
    private ICategoryWriteRepository _writeRepository;

    public DeleteCategoryCommandHandler(ICategoryReadRepository readRepository, ICategoryWriteRepository writeRepository)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
    }

    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _readRepository.Get(request.Id);
        if (category == null)
        {
            throw new CustomApiException("Erro ao deletar categoria", StatusCodes.Status404NotFound,
                "Categoria não encontrada");
        }

        var subCategories =
            await _readRepository.FilterCategories(new CategoriesQuery { ParentId = category.Id, Limit = int.MaxValue });

        foreach (var subCategory in subCategories)
        {
            await _writeRepository.Deactivate(subCategory);
        }
        await _writeRepository.Deactivate(category);
    }
}