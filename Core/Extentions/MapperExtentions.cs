using AutoMapper;
using Core.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extentions
{
    public static class MapperExtentions
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CreateTierProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
