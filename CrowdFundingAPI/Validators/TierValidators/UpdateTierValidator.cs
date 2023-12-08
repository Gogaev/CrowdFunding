using Domain.Features.TierFeature.Commands;
using FluentValidation;

namespace CrowdFundingAPI.Validators.TierValidators
{
    public class UpdateTierValidator : AbstractValidator<UpdateTierCommand>
    {
        public UpdateTierValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.RequiredMoney)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Amount of money must be greater then 0");
            RuleFor(x => x.Benefit)
                .NotEmpty()
                .Length(0, 100)
                .WithMessage("Tier Name must be bugger than 0 and less then 100 symbols");
        }
    }
}
