using AutoMapper;
using Domain.DomainModels.Entities;
using Domain.Features.UserFeatures.Commands;

namespace Domain.Mapper.UserMapping
{
    public class RegisterUserProfile : Profile
    {
        public RegisterUserProfile()
        {
            CreateMap<RegisterUserCommand, ApplicationUser>()
               .ForMember(t =>
                   t.FullName,
                   ctd => ctd.MapFrom(src => src.FullName))
               .ForMember(t =>
                   t.UserName,
                   ctd => ctd.MapFrom(src => src.UserName))
               .ForMember(t =>
                   t.Description,
                   ctd => ctd.MapFrom(src => src.Description))
               .ForMember(t =>
                   t.Email,
                   ctd => ctd.MapFrom(src => src.EmailAddress));
        }
    }
}
