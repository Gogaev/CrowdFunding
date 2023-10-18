using AutoMapper;
using Domain.DomainModels;
using Domain.Features.TierFeature.Commands;

namespace Domain.Mapper.TierMapping
{
    public class CreateTierProfile : Profile
    {
        public CreateTierProfile()
        {
            CreateMap<CreateTierCommand, Tier>()
                .ForMember(dest =>
                    dest.TierName,
                    opt => opt.MapFrom(src => src.TierName))
                .ForMember(dest =>
                    dest.Benefit,
                    opt => opt.MapFrom(src => src.Benefit))
                .ForMember(dest =>
                    dest.RequiredMoney,
                    opt => opt.MapFrom(src => src.RequiredMoney))
                .ForMember(dest =>
                    dest.ProjectId,
                    opt => opt.MapFrom(src => src.ProjectId));
        }
    }
}