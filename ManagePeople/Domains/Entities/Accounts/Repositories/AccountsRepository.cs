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
    IOptionsSnapshot<StoredProcedureOptions> storedProcedures) : IPersonsRepository
    {
        public Task<PersonModel?> CreateAsync(PersonModel person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int personId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PersonModel>> RetrieveAllAsync(string? personName)
        {
            throw new NotImplementedException();
        }

        public Task<PersonModel?> RetrieveSingleAsync(int personId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int personId, PersonModel team)
        {
            throw new NotImplementedException();
        }
    }
}
