using Domain.Features.UserFeatures.Commands;
using FluentValidation;

namespace CrowdFundingAPI.Validators.UserValidators
{
    public class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("User Name is required");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
