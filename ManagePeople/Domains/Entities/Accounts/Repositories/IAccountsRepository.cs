using ManagePeople.Libraries.Shared;

namespace ManagePeople.Domains.Entities.Accounts.Repositories
{
    public interface IAccountsRepository
    {
        Task<List<AccountModel>> RetrieveAllAsync(int personId);

        Task<AccountModel?> CreateAsync(int personId, AccountModel account);
        Task<bool> DeleteAsync(int accountId);
        Task<AccountModel?> RetrieveSingleAsync(int accountId);

        Task<bool> UpdateAsync(int personId, int accountId, AccountModel account);
    }
}
