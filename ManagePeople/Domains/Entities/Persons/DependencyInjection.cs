using ManagePeople.Domains.Entities.Persons.Repositories;

namespace ManagePeople.Domains.Entities.Persons
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersonServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonsRepository, PersonsRepository>();
            return services;
        }
    }
}
