using Microsoft.Extensions.DependencyInjection;
using IMapper = CQRS.Application.Interfaces.AutoMapper.IMapper;

namespace CQRS.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, Mapper>();
        }
    }
}
