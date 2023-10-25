using AutoMapper;
using Core.Dtos.Project;
using Domain.DomainModels.Entities;

namespace Domain.Mapper.ProjectMapping
{
    public class PublishedProjectProfile : Profile
    {
        public PublishedProjectProfile()
        {
            CreateMap<Project, PublishedProjectDto>()
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
                    opt => opt.MapFrom(src => src.Creator != null ? src.Creator.FullName : null))
                .ForMember(dest =>
                    dest.Supporters,
                    opt => opt.MapFrom(src => src.Supporters.Select(src => src.User != null ? src.User.FullName : null).ToList()));
        }
    }
}
