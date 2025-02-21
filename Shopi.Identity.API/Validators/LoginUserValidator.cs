using FluentValidation;
using Shopi.Identity.API.Models;

namespace Shopi.Identity.API.Validators;

public class LoginUserValidator : AbstractValidator<LoginUser>
{
    public LoginUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email inválido");
        RuleFor(x => x.Password).NotEmpty()
            .WithMessage("Senha não pode estar vazia");
    }
}