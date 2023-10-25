using Domain.Features.ProjectFeatures.Commands;
using FluentValidation;

namespace CrowdFundingAPI.Validators.ProjectValidators
{
    public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required!")
                .Length(0, 50).WithMessage("Title Must be less than 50 simbols");
            RuleFor(x => x.Description)
                .Length(0, 2000)
                .WithMessage("Description must be less than 2000 simbols");
            //RuleFor(x => x.LastDay)
            //    .NotEmpty();
            RuleFor(x => x.RequiredMoney)
                .NotNull()
                .GreaterThan(0).WithMessage("Required ammount must be greater than 0");
        }
    }
}
