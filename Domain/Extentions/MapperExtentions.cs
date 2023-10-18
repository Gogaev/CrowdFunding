using AutoMapper;
using Domain.Mapper.TierMapping;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extentions
{
    public static class MapperExtentions
    {
        public static void ConfigureMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new TierProfile());
                mc.AddProfile(new CreateTierProfile());
                mc.AddProfile(new UpdateTierProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
