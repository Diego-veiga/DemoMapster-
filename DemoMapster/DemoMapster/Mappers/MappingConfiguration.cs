using DemoMapster.DTOs;
using DemoMapster.Model;
using Mapster;

namespace DemoMapster.Mappers
{
    public static class MappingConfiguration
    {
        public static IServiceCollection RegisterMaps(this IServiceCollection services)
        {
            services.AddMapster();
            TypeAdapterConfig<Person, PersonViewModel>
                .NewConfig()
                .Map(p => p.CompletedName, d => $"{d.FirstName} {d.LastName}")
                .Map(p => p.Age, d => d.Age);
            return services;
        }
    }
}
