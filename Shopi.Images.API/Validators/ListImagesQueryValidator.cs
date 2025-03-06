using FluentValidation;
using Shopi.Images.API.Queries;

namespace Shopi.Images.API.Validators;

public class ListImagesQueryValidator: AbstractValidator<ListImagesQuery>
{
    public ListImagesQueryValidator()
    {
        RuleFor(q => q.ProductId).NotNull().WithMessage("Id não pode estar vazio");
        RuleFor(q => q.PageNumber).GreaterThan(0).WithMessage("Número da página deve ser maior que 0");
        RuleFor(q => q.Limit).GreaterThan(0).WithMessage("Limite deve ser maior que 0");
    }
}