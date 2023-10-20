using AutoMapper;
using Core.Dtos.User;
using Domain.DomainModels.Entities;

namespace Domain.Mapper.UserMapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(t =>
                    t.Id,
                    ctd => ctd.MapFrom(src => src.Id))
                .ForMember(t =>
                    t.Name,
                    ctd => ctd.MapFrom(src => src.UserName))
                .ForMember(t =>
                    t.Description,
                    ctd => ctd.MapFrom(src => src.Description))
                .ForMember(t =>
                    t.FullName,
                    ctd => ctd.MapFrom(src => src.FullName))
                .ForMember(t =>
                    t.Email,
                    ctd => ctd.MapFrom(src => src.Email));
        }
    }
}
