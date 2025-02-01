using ManagePeople.Domains.Entities.Accounts.Repositories;
using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Domains.Entities.Transactions.Repositories;

namespace ManagePeople
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIncidentManagementServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountsRepository, AccountsRepository>();
            services.AddScoped<IPersonsRepository, PersonsRepository();
            services.AddScoped<ITransactionsRepository, TransactionsRepository>();

            return services;
        }
    }
}
