using ManagePeople.Libraries.Shared;

namespace ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface
{
    public interface IPersonClientService
    {
        Task<PersonModel?> CreatePersonAsync(PersonModel person);

        Task<bool> DeletePersonAsync(int personId);

        //Task<EscalationModel> GetEscalationByTeamIdAsync(int teamId, int escalationTypeId);

        Task<PersonModel?> GetPersonByPersonIdAsync(int personId);

        Task<List<PersonModel>> GetPersonsAsync();

        Task<bool> UpdatePersonAsync(PersonModel person);
    }
}
