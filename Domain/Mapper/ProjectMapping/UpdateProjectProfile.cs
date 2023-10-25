using AutoMapper;
using Domain.DomainModels.Entities;
using Domain.Features.ProjectFeatures.Commands;

namespace Domain.Mapper.ProjectMapping
{
    public class UpdateProjectProfile : Profile
    {
        public UpdateProjectProfile()
        {
            CreateMap<UpdateProjectCommand, Project>()
                .ForMember(dest =>
                    dest.Title,
                    opt => opt.MapFrom(src => src.Title))
                .ForMember(dest =>
                    dest.Description,
                    opt => opt.MapFrom(src => src.Description))
                .ForMember(dest =>
                    dest.ImageUrl,
                    opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest =>
                    dest.LastDay,
                    opt => opt.MapFrom(src => src.LastDay))
                .ForMember(dest =>
                    dest.RequiredMoney,
                    opt => opt.MapFrom(src => src.RequiredMoney))
                .ForMember(dest =>
                    dest.InvestedMoney,
                    opt => opt.MapFrom(src => src.InvestedMoney))
                .ForMember(dest =>
                    dest.Status,
                    opt => opt.MapFrom(src => src.Status));
        }
    }
}
