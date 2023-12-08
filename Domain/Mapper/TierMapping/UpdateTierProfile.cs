using AutoMapper;
using Domain.DomainModels.Entities;
using Domain.Features.TierFeature.Commands;

namespace Domain.Mapper.TierMapping
{
    public class UpdateTierProfile : Profile
    {
        public UpdateTierProfile()
        {
            CreateMap<UpdateTierCommand, Tier>()
                .ForMember(dest =>
                        dest.Benefit,
                    opt => opt.MapFrom(src => src.Benefit))
                .ForMember(dest =>
                        dest.RequiredMoney,
                    opt => opt.MapFrom(src => src.RequiredMoney));
        }
    }
}
