using Domain.Features.TierFeature.Commands;
using FluentValidation;

namespace CrowdFundingAPI.Validators.TierValidators
{
    public class CreateTierValidator : AbstractValidator<CreateTierCommand>
    {
        public CreateTierValidator()
        {
            RuleFor(x => x.TierName)
                .NotEmpty()
                .Length(0, 20)
                .WithMessage("Tier Name must be bugger than 0 and less then 20 simbols");
            RuleFor(x => x.RequiredMoney)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Ammount of money must be greater then 0");
            RuleFor(x => x.Benefit)
                .NotEmpty()
                .Length(0, 100)
                .WithMessage("Tier Name must be bugger than 0 and less then 100 simbols");
            RuleFor(x => x.ProjectId)
                .NotEmpty();
        }
    }
}
