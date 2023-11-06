using AutoMapper;
using Domain.Mapper.ProjectMapping;
using Domain.Mapper.TierMapping;
using Domain.Mapper.UserMapping;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions
{
    public static class MapperExtensions
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TierProfile());
                mc.AddProfile(new CreateTierProfile());
                mc.AddProfile(new UpdateTierProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new RegisterUserProfile());
                mc.AddProfile(new CreateProjectProfile());
                mc.AddProfile(new UpdateProjectProfile());
                mc.AddProfile(new PublishedProjectProfile());
                mc.AddProfile(new GetProjectByIdProfile());
                mc.AddProfile(new ProjectProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
