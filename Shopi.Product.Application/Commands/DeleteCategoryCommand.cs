using MediatR;

namespace Shopi.Product.Application.Commands;

public class DeleteCategoryCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteCategoryCommand()
    {
    }

    public DeleteCategoryCommand(Guid id)
    {
        Id = id;
    }
}