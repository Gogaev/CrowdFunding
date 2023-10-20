using AutoMapper;
using Core.Dtos.Project;
using Domain.DomainModels.Entities;

namespace Domain.Mapper.ProjectMapping
{
    public class GetProjectByIdProfile : Profile
    {
        public GetProjectByIdProfile()
        {
            CreateMap<Project, ProjectWithTiersDto>()
                .ForMember(dest =>
                    dest.Id,
                    opt => opt.MapFrom(src => src.Id))
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
                    dest.InvestedMoney,
                    opt => opt.MapFrom(src => src.InvestedMoney))
                .ForMember(dest =>
                    dest.CreatorName,
                    opt => opt.MapFrom(src => src.Creator.FullName))
                .ForMember(dest =>
                    dest.Tiers,
                    opt => opt.MapFrom(src => src.Tiers));
        }
    }
}
