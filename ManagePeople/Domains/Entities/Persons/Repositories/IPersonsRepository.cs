using ManagePeople.Libraries.Shared;

namespace ManagePeople.Domains.Entities.Persons.Repositories
{
    public interface IPersonsRepository
    {
        Task<PersonModel?> CreateAsync(PersonModel person);

        Task<List<PersonModel>> RetrieveAllAsync();

        Task<PersonModel?> RetrieveSingleAsync(int personId);

        Task<bool> UpdateAsync(int personId, PersonModel person);

        Task<bool> DeleteAsync(int personId);
    }
}
