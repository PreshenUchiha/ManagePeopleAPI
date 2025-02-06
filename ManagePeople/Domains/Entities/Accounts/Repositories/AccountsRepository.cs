using AutoMapper;
using Dapper;
using ManagePeople.Configuration;
using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Libraries.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Data;

namespace ManagePeople.Domains.Entities.Accounts.Repositories
{
    public class AccountsRepository(
    ILogger<AccountsRepository> logger,
    IOptionsSnapshot<ConnectionStringsOptions> connectionStrings,
    IOptionsSnapshot<StoredProcedureOptions> storedProcedures) : IAccountsRepository
    {
        public async Task<AccountModel?> CreateAsync(int personId, AccountModel account)
        {
            logger.LogInformation(
                "Repository => Attempting to add a new account to person {Person}",
                personId);

            var dynamicParams = new DynamicParameters();

            dynamicParams.Add(name: "@AccountId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParams.Add(name: "@PersonId", value: personId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@AccountNumber", value: account.AccountNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@OutstandingBalance", value: account.OutstandingBalance, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.InsertNewAccount,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);

                account.AccountId = dynamicParams.Get<int>("@AccountId");

                logger.LogInformation(
                    "{Announcement}: Attempt to add a new account to person {Person} completed successfully with id {Account}",
                    "SUCCEEDED", personId, account.AccountId);

                return account;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to add a new account to person {Person} was unsuccessful",
                    "FAILED", personId);

                return null;
            }
        }


        public async Task<List<AccountModel>> RetrieveAllAsync(int personId)
        {
            logger.LogInformation(
    "Repository => Attempting to retrieve all accounts for person {Person}",
    personId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            var accounts = new List<AccountModel>();

            try
            {
                accounts =
                    (await sqlConnection.QueryAsync<AccountModel>(
                        sql: storedProcedures.Value.GetAllAccountsByPersonId,
                        param: new { personId },
                        commandType: CommandType.StoredProcedure))
                        .ToList();

                logger.LogInformation(
                    "{Announcement}: Attempt to retrieve all team accounts for person {Person} completed successfully",
                    "SUCCEEDED", personId);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to retrieve all team accounts for person {Person} was unsuccessful",
                    "FAILED", personId);
            }

            return accounts;
        }


        public async Task<AccountModel?> RetrieveSingleAsync(int accountId)
        {
            logger.LogInformation(
            "Repository => Attempting to retrieve account {Account}",
            accountId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            AccountModel? account = null;

            try
            {
                account =
                    await sqlConnection.QuerySingleOrDefaultAsync<AccountModel>(
                        sql: storedProcedures.Value.GetAccountById,
                        param: new { accountId },
                        commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to retrieve account {Account} completed successfully",
                    "SUCCEEDED", accountId);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to retrieve person {Account} was unsuccessful",
                    "FAILED", accountId);

                account = null;
            }

            return account;
        }

       public async Task<bool> UpdateAsync(int personId, int accountId, AccountModel account)
        {
            logger.LogInformation(
                "Repository => Attempting to update account {Account} from person {Person}",
                accountId, personId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.UpdateAccountById,
                    param: new
                    {
                        accountId,
                        personId,
                        account.AccountNumber,
                        account.OutstandingBalance
                    },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt the update account {Account} from person {Person} completed successfully",
                    "SUCCEEDED", accountId, personId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to update account {Account} from person {Person} was unsuccessful",
                    "FAILED", accountId, personId);

                return false;
            }
        }

        public async Task<bool> DeleteAsync(int accountId)
        {
            logger.LogInformation(
    "Repository => Attempting to delete person {Person}",
         accountId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.DeleteAccountById,
                    param: new { accountId },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to delete person {Person} completed successfully",
                    "SUCCEEDED", accountId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to delete person {Person} was unsuccessful",
                    "FAILED", accountId);

                return false;
            }
        }



    }
}