using FluentValidation;
using Shopi.Images.API.Queries;

namespace Shopi.Images.API.Validators;

public class GetImageQueryValidator : AbstractValidator<GetImageQuery>
{
    public GetImageQueryValidator()
    {
        RuleFor(q => q.Id).MinimumLength(24).WithMessage("Id não pode ter menos que 24 caracteres").NotNull().WithMessage("Id não pode estar vazio");
    }
}