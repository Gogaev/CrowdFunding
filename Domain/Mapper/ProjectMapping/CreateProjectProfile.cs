using AutoMapper;
using Domain.DomainModels.Entities;
using Domain.Features.ProjectFeatures.Commands;

namespace Domain.Mapper.ProjectMapping
{
    public class CreateProjectProfile : Profile
    {
        public CreateProjectProfile()
        {
            CreateMap<CreateProjectCommand, Project>()
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
                    opt => opt.MapFrom(src => src.RequiredMoney));
        }
    }
}
