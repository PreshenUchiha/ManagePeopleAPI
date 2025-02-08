using Dapper;
using ManagePeople.Configuration;
using ManagePeople.Domains.Entities.Accounts.Repositories;
using ManagePeople.Libraries.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System.Data;
using System.Security.Principal;

namespace ManagePeople.Domains.Entities.Transactions.Repositories
{
    public class TransactionsRepository(
    ILogger<TransactionsRepository> logger,
    IOptionsSnapshot<ConnectionStringsOptions> connectionStrings,
    IOptionsSnapshot<StoredProcedureOptions> storedProcedures) : ITransactionsRepository
    {
        public async Task<TransactionModel?> CreateAsync(int accountId, TransactionModel transaction)
        {
            logger.LogInformation(
                "Repository => Attempting to add a new transaction to account {Account}",
                accountId);

            var dynamicParams = new DynamicParameters();

            dynamicParams.Add(name: "@TransactionId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParams.Add(name: "@AccountId", value: accountId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@TransactionDate", value: transaction.TransactionDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@CaptureDate", value: transaction.CaptureDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@Amount", value: transaction.Amount, dbType: DbType.Decimal, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@Description", value: transaction.Description, dbType: DbType.String, direction: ParameterDirection.Input);


            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.InsertNewTransaction,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);

                transaction.TransactionId = dynamicParams.Get<int>("@TransactionId");

                logger.LogInformation(
                    "{Announcement}: Attempt to add a new transaction to account {Account} completed successfully with id {Transaction}",
                    "SUCCEEDED", accountId, transaction.TransactionId);

                return transaction;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to add a new transaction to account {Account} was unsuccessful",
                    "FAILED", accountId);

                return null;
            }
        }
        public async Task<List<TransactionModel>> RetrieveAllAsync(int accountId)
        {
            logger.LogInformation(
        "Repository => Attempting to retrieve all transactions for account {Account}",
          accountId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            var transactions = new List<TransactionModel>();

            try
            {
                transactions =
                    (await sqlConnection.QueryAsync<TransactionModel>(
                        sql: storedProcedures.Value.GetAllTransactionsByAccountId,
                        param: new { accountId },
                        commandType: CommandType.StoredProcedure))
                        .ToList();

                logger.LogInformation(
                    "{Announcement}: Attempt to retrieve all transactions for account {Account} completed successfully",
                    "SUCCEEDED", accountId);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to retrieve all ttransactions for account {Account} was unsuccessful",
                    "FAILED", accountId);
            }

            return transactions;
        }

        public async Task<TransactionModel?> RetrieveSingleAsync(int transactionId)
        {
            logger.LogInformation(
            "Repository => Attempting to retrieve transactions {Transactions}",
            transactionId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            TransactionModel? transaction = null;

            try
            {
                transaction =
                    await sqlConnection.QuerySingleOrDefaultAsync<TransactionModel>(
                        sql: storedProcedures.Value.GetTransactionById,
                        param: new { transactionId },
                        commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to retrieve transactions {Transactions} completed successfully",
                    "SUCCEEDED", transactionId);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to retrieve transactions {Transactions} was unsuccessful",
                    "FAILED", transactionId);

                transaction = null;
            }

            return transaction;
        }

        public async Task<bool> UpdateAsync(int accountId, int transactionId, TransactionModel transactionModel)
        {
            logger.LogInformation(
                "Repository => Attempting to update transaction {Transaction} from account {Account}",
                transactionId, accountId);
            logger.LogInformation(storedProcedures.Value.UpdateTransactionById);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.UpdateTransactionById,
                    param: new
                    {
                        TransactionId = transactionId,
                        AccountId = accountId,
                        transactionModel.TransactionDate,
                        transactionModel.CaptureDate,
                        transactionModel.Amount,
                        transactionModel.Description
                    },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt the update transaction {Transaction} from account {Account} completed successfully",
                    "SUCCEEDED", transactionId, accountId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to update transaction {Transaction} from account {Account} was unsuccessful",
                    "FAILED", transactionId, accountId);

                return false;
            }
        }

        public async Task<bool> DeleteAsync(int transactionId)
        {
            logger.LogInformation(
    "Repository => Attempting to delete transaction {Transaction}",
         transactionId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.DeleteTransactionById,
                    param: new { transactionId },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to delete transaction {Transaction} completed successfully",
                    "SUCCEEDED", transactionId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to delete transaction {Transaction} was unsuccessful",
                    "FAILED", transactionId);

                return false;
            }
        }
    }
}
