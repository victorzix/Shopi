using FluentValidation;
using Shopi.Customer.Application.Commands;

namespace Shopi.Customer.API.Validators;

public class UpdateAddressValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressValidator()
    {
        RuleFor(a => a.State).MaximumLength(3).WithMessage("Estado pode ter no máximo 3 caracteres");
        RuleFor(a => a.ZipCode).MaximumLength(12).WithMessage("CEP ultrapassa a quantidade de caracteres permitidos");
    }
}