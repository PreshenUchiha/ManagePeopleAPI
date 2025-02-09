using ManagePeopleApp.Frontends.MVC.Domains.Persons.APIClient;
using ManagePeopleApp.Frontends.MVC.Domains.Persons.Carriers;
using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;

namespace ManagePeopleApp.Frontends.MVC.Domains.Persons;
public static class DependencyInjection
{
    public static IServiceCollection AddPersonServices(this IServiceCollection services)
    {
        services.AddScoped<IPersonClientService, PersonClientService>();

        services.AddScoped<IPersonsClientsCarrier, PersonsClientsCarrier>();

        return services;
    }
}

