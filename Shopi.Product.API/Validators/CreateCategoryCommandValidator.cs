using FluentValidation;
using Shopi.Product.API.Commands;

namespace Shopi.Product.API.Validators;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(c => c.Name).NotNull().WithMessage("Nome da categoria não pode estar vazio");
    }
}