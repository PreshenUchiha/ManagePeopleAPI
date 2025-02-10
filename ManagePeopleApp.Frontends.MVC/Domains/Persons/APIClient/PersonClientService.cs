using HttpClientLibrary.HttpClientService;
using ManagePeople.Libraries.Shared;
using ManagePeopleApp.Frontends.MVC.Configuration;
using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;
using ManagePeopleApp.Frontends.MVC.Helpers;
using Microsoft.Extensions.Options;

namespace ManagePeopleApp.Frontends.MVC.Domains.Persons.APIClient
{
    public class PersonClientService(
    ILogger<PersonClientService> logger,
    IHttpClientHelper client,
    IOptionsSnapshot<ApiEndpointsConfiguration> apiEndpoints) : IPersonClientService
    {
        public async Task<PersonModel?> CreatePersonAsync(PersonModel person)
        {
            try
            {
                //var url = apiEndpoints.Value.CreatePersonsEndpoint;
                var url = "api/persons";

                var response = await client.HttpPostAsync(url, person);

                var result = await response.Content.ReadFromJsonAsync<PersonModel>();

                return result!;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "PersonsClientService => {Announcement}: Attempt to create person was unsuccessful",
                    LoggerConstants.Failed);

                return null;
            }
        }

        public async Task<bool> DeletePersonAsync(int personId)
        {
            try
            {
                var url = apiEndpoints.Value.DeletePersonsEndpoint
                                            .Replace(ApiParameterConstants.PersonId, personId.ToString());

                await client.HttpDeleteAsync(url);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "PersonsClientService => {Announcement}: Attempt to delete person was unsuccessful",
                    LoggerConstants.Failed);

                return false;
            }
        }

        public async Task<PersonModel?> GetPersonByPersonIdAsync(int personId)
        {
            try
            {
                var url = apiEndpoints.Value.GetPersonByPersonIdEndpoint
                                            .Replace(ApiParameterConstants.PersonId, personId.ToString());

                var result = await client.HttpRetrieveByIdAsync<PersonModel>(url);

                return result!;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "PersonsClientService => {Announcement}: Attempt to retrieve person was unsuccessful",
                    LoggerConstants.Failed);

                return null;
            }
        }

        public async Task<List<PersonModel>> GetPersonsAsync()
        {
            try
            {
                var url = apiEndpoints.Value.GetPersonsEndpoint;

                var result = await client.HttpRetrieveAllAsync<PersonModel>(url);

                return result!;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "PersonsClientService => {Announcement}: Attempt to retrieve persons was unsuccessful",
                    LoggerConstants.Failed);

                return [];
            }
        }
        public async Task<bool> UpdatePersonAsync(PersonModel person)
        {
            try
            {
                var url = apiEndpoints.Value.UpdatePersonsEndpoint
                                            .Replace(ApiParameterConstants.PersonId, person.PersonId.ToString());

                await client.HttpPutAsync(url, person);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "PersonsClientService => {Announcement}: Attempt to update person was unsuccessful",
                    LoggerConstants.Failed);

                return false;
            }
        }
    }
}
