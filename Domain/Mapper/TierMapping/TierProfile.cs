using AutoMapper;
using Core.Dtos.Tier;
using Domain.DomainModels.Entities;

namespace Domain.Mapper.TierMapping
{
    public class TierProfile : Profile
    {
        public TierProfile()
        {
            CreateMap<Tier, TierDto>()
                .ForMember(t => 
                    t.Id,
                    ctd => ctd.MapFrom(src => src.Id))
                .ForMember(t =>
                    t.TierName,
                    ctd => ctd.MapFrom(src => src.TierName))
                .ForMember(t =>
                    t.RequiredMoney,
                    ctd => ctd.MapFrom(src => src.RequiredMoney))
                .ForMember(t =>
                    t.Benefit,
                    ctd => ctd.MapFrom(src => src.Benefit))
                .ForMember(t =>
                    t.IsReached,
                    ctd => ctd.MapFrom(src => src.IsReached));
        }
    }
}