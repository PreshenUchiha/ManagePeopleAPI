using Dapper;
using ManagePeople.Configuration;
using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Libraries.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace ManagePeople.Domains.Entities.Accounts.Repositories
{
    public class AccountsRepository(
    ILogger<AccountsRepository> logger,
    IOptionsSnapshot<ConnectionStringsOptions> connectionStrings,
    IOptionsSnapshot<StoredProcedureOptions> storedProcedures) : IAccountsRepository
    {

    }
    
}
