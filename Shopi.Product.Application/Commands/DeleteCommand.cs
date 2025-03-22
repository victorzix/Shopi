using MediatR;

namespace Shopi.Product.Application.Commands;

public class DeleteCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteCommand()
    {
    }

    public DeleteCommand(Guid id)
    {
        Id = id;
    }
}