using ManagePeople.Domains.Entities.Accounts.Repositories;
using ManagePeople.Domains.Entities.Transactions.Repositories;

namespace ManagePeopleAPI.Domains.Entities.Transactions;

public static class DependencyInjection
{
    public static IServiceCollection AddTransactionServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionsRepository, TransactionsRepository>();

        return services;
    }
}

