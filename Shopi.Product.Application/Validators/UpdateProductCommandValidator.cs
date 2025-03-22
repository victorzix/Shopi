using FluentValidation;
using Shopi.Product.Application.Commands;

namespace Shopi.Product.Application.Validators;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(p => p.Price).GreaterThan(0)
            .WithMessage("Preço do produto deve ser maior que zero");
        RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0)
            .WithMessage("Quantidade de produtos deve ser maior ou igual a zero");
    }
}