using FluentValidation;
using Shopi.Product.Application.Commands.ProductsCommands;

namespace Shopi.Product.Application.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Name).NotNull().WithMessage("Nome do produto não pode estar vazio");
        RuleFor(p => p.Description).NotNull().WithMessage("Descrição do produto não pode estar vazia");
        RuleFor(p => p.Price).NotNull().WithMessage("Preço do produto não pode estar vazio").GreaterThan(0).WithMessage("Preço do produto deve ser maior que zero");
        RuleFor(p => p.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantidade de produtos deve ser maior ou igual a zero");
        RuleFor(p => p.Manufacturer).NotNull().WithMessage("Nome do fabricante do produto não pode estar vazio");
    }
}