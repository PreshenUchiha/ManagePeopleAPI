using ManagePeople.Libraries.Shared;

namespace ManagePeople.Domains.Entities.Persons.Repositories
{
    public interface IPersonsRepository
    {
        Task<PersonModel?> CreateAsync(PersonModel person);

        Task<List<PersonModel>> RetrieveAllAsync();

        Task<PersonModel?> RetrieveSingleAsync(int code);

        Task<bool> UpdateAsync(int code, PersonModel person);

        Task<bool> DeleteAsync(int code);
    }
}
