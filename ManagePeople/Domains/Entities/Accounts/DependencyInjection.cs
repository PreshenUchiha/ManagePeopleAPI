using ManagePeople.Domains.Entities.Accounts.Repositories;

namespace ManagePeople.Domains.Entities.Accounts
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAccountServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountsRepository, AccountsRepository>();

            return services;
        }
    }
}
