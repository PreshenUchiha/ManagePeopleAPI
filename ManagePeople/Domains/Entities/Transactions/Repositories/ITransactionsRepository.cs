using ManagePeople.Libraries.Shared;

namespace ManagePeople.Domains.Entities.Transactions.Repositories
{
    public interface ITransactionsRepository
    {
        Task<TransactionModel?> RetrieveSingleAsync(int transactionId);

        Task<List<TransactionModel>> RetrieveAllAsync(int accountId);

        Task<TransactionModel?> CreateAsync(int accountId, TransactionModel transaction);

        Task<bool> UpdateAsync(int accountId, int transactionId, TransactionModel transactionModel);

        Task<bool> DeleteAsync(int transactionId);
    }
}
 