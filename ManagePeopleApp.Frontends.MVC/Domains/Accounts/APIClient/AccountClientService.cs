using global::ManagePeopleApp.Frontends.MVC.Configuration;
using global::ManagePeopleApp.Frontends.MVC.Helpers;
using HttpClientLibrary.HttpClientService;
using ManagePeople.Libraries.Shared;
using ManagePeopleApp.Frontends.MVC.Domains.Accounts.Interface;
using Microsoft.Extensions.Options;

namespace ManagePeopleApp.Frontends.MVC.Domains.Accounts.APIClient
{
    public class AccountClientService(
            ILogger<AccountClientService> logger,
            IHttpClientHelper client,
            IOptionsSnapshot<ApiEndpointsConfiguration> apiEndpoints) : IAccountClientService
        {
            public async Task<List<AccountModel>> GetAccountsAsync(int personId)
            {
                try
                {
                    var url = apiEndpoints.Value.GetAccountsByPersonIdEndpoint
                                                .Replace(ApiParameterConstants.PersonId, personId.ToString());

                    var result = await client.HttpRetrieveAllAsync<AccountModel>(url);

                    return result;
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "{Annoucement}: Attempt to get accounts was unsuccessful",
                        LoggerConstants.Failed);

                    return [];
                }
            }

            public async Task<AccountModel?> GetAccountByAccountIdAsync(
                int accountId,
                int personId)
            {
                try
                {
                    string RequestUri = apiEndpoints.Value.GetAccountsByPersonIdEndpoint
                                                          .Replace("{accountId}", accountId.ToString())
                                                          .Replace(ApiParameterConstants.PersonId, personId.ToString());

                    var result = await client.HttpRetrieveByIdAsync<AccountModel>(RequestUri);

                    return result;
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "{Announcement}: Attempt to get a account member was unsuccessful",
                        LoggerConstants.Failed);

                    return null;
                }
            }

            public async Task<bool> DeleteAccountAsync(
                int accountId,
                int personId)
            {
                try
                {
                    string RequestUri = apiEndpoints.Value.DeleteAccountEndpoint
                                                          .Replace("{accountId}", accountId.ToString())
                                                          .Replace(ApiParameterConstants.PersonId, personId.ToString());

                    await client.HttpDeleteAsync(RequestUri);

                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogError(
                          ex,
                          "{Announcement}: Attempt to delete a account was unsuccessful",
                          LoggerConstants.Failed);

                    return false;
                }
            }

            public async Task<bool> CreateTeamMemberAsync(AccountModel model)
            {
                try
                {
                    string RequestUri = apiEndpoints.Value.CreateAccountEndpoint
                                                          .Replace(ApiParameterConstants.PersonId, model.AccountId.ToString());
                    await client.HttpPostAsync(RequestUri, model);
                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "{Announcement}: Attempt to create an account was unsuccessful",
                        LoggerConstants.Failed);

                    return false;
                }
            }

            public async Task<bool> UpdateTeamMemberAsync(AccountModel model)
            {
                try
                {
                    string RequestUri = apiEndpoints.Value.UpdateAccountEndpoint
                                                          .Replace("{accountId}", model.AccountId.ToString())
                                                          .Replace(ApiParameterConstants.PersonId, model.PersonId.ToString());

                    await client.HttpPutAsync(RequestUri, model);
                    return true;
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "{Announcement}: Attempt to update an account was unsuccessful",
                        LoggerConstants.Failed);

                    return false;
                }
            }
        }
    }


