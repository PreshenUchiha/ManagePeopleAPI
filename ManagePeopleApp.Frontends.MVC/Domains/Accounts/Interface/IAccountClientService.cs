using ManagePeople.Libraries.Shared;

namespace ManagePeopleApp.Frontends.MVC.Domains.Accounts.Interface
{
    public interface IAccountClientService
    {
        Task<List<AccountModel>> GetAccountsAsync(int personId);
        Task<AccountModel?> GetAccountByAccountIdAsync(int accountId, int personId);

        Task<bool> DeleteAccountAsync(int accountId, int personId);

        Task<bool> CreateTeamMemberAsync(AccountModel model);
        Task<bool> UpdateTeamMemberAsync(AccountModel model);
    }
}
