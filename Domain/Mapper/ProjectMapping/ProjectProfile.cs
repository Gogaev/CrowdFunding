﻿using AutoMapper;
using Core.Dtos.Project;
using Domain.DomainModels.Entities;

namespace Domain.Mapper.ProjectMapping
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
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
                    dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest =>
                    dest.InvestedMoney,
                    opt => opt.MapFrom(src => src.InvestedMoney))
                .ForMember(dest =>
                        dest.RequiredMoney,
                    opt => opt.MapFrom(src => src.RequiredMoney))
                .ForMember(dest =>
                    dest.CreatorName,
                    opt => opt.MapFrom(src => src.Creator != null ? src.Creator.FullName : null))
                .ForMember(dest =>
                    dest.Supporters,
                    opt => opt.MapFrom(src => src.Supporters.Select(user => user.User != null ? user.User.FullName : null).ToList()));
        }
    }
}
