using FluentValidation;
using Shopi.Images.Application.Queries;

namespace Shopi.Images.Application.Validators;

public class GetImageQueryValidator : AbstractValidator<GetImageQuery>
{
    public GetImageQueryValidator()
    {
        RuleFor(q => q.Id).MinimumLength(24).WithMessage("Id não pode ter menos que 24 caracteres").NotNull().WithMessage("Id não pode estar vazio");
    }
}