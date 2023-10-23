using Core.Dtos.User;
using FluentValidation;

namespace CrowdFundingAPI.Validators.UserValidator
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .Length(0, 20)
                .WithMessage("User name is required!");
            RuleFor(x => x.FullName)
                .NotEmpty()
                .Length(0, 30)
                .WithMessage("Name and Surname is Required");
            RuleFor(x => x.Description)
                .Length(0, 500)
                .WithMessage("Description should be less than 500 simbols");
            RuleFor(x => x.EmailAddress)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is required");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}
