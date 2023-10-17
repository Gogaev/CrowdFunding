using AutoMapper;
using Core.Dtos.Tier;
using Domain.DomainModels;

namespace Domain.Mapper
{
    public class CreateTierProfile : Profile
    {
        public CreateTierProfile()
        {
            CreateMap<CreateTierDto, Tier>()
                .ForMember(t => t.TierName,
                ctd => ctd.MapFrom(src => src.TierName))
                .ForMember(t => t.RequiredMoney,
                ctd => ctd.MapFrom(src => src.RequiredMoney))
                .ForMember(t => t.Benefit,
                ctd => ctd.MapFrom(src => src.Benefit))
                .ForMember(t => t.ProjectId,
                ctd => ctd.MapFrom(src => src.ProjectId));
        }
    }
}
