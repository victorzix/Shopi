using FluentValidation;
using Shopi.Identity.Domain.Entities;

namespace Shopi.Identity.Application.Validators;

public class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido");
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("Senha não pode estar vazia");
    }
}