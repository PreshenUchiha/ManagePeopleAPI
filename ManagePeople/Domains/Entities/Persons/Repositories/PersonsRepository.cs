using Dapper;
using ManagePeople.Configuration;
using ManagePeople.Libraries.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace ManagePeople.Domains.Entities.Persons.Repositories
{

    public class PersonsRepository(
    ILogger<PersonsRepository> logger,
    IOptionsSnapshot<ConnectionStringsOptions> connectionStrings,
    IOptionsSnapshot<StoredProcedureOptions> storedProcedures) : IPersonsRepository
    {

        public async Task<PersonModel?> CreateAsync(PersonModel person)
        {
            logger.LogInformation("Repository => Attempting to create a new person");

            var dynamicParams = new DynamicParameters();

            dynamicParams.Add(name: "@PersonId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParams.Add(name: "@Name", value: person.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@Surname", value: person.Surname, dbType: DbType.String, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@IdNumber", value: person.IdNumber, dbType: DbType.String, direction: ParameterDirection.Input);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.InsertNewPerson,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);

                person.PersonId = dynamicParams.Get<int>("@PersonId");

                logger.LogInformation(
                    "{Announcement}: Attempt to create a new person completed successfully with id {Person}",
                    "SUCCEEDED", person.PersonId);

                return person;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to create a new person was unsuccessful",
                    "FAILED");

                return null;
            }
        }

        public async Task<bool> DeleteAsync(int personId)
        {
            logger.LogInformation(
    "Repository => Attempting to delete person {Person}",
         personId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.DeletePersonById,
                    param: new { personId },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to delete person {Person} completed successfully",
                    "SUCCEEDED", personId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to delete person {Person} was unsuccessful",
                    "FAILED", personId);

                return false;
            }
        }


        public async Task<List<PersonModel>> RetrieveAllAsync()
        {
            logger.LogInformation("Repository => Attempting to retrieve all persons");

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            var persons = new List<PersonModel>();

            try
            {
                persons =
                    (await sqlConnection.QueryAsync<PersonModel>(
                        sql: storedProcedures.Value.GetAllPersons,
                        commandType: CommandType.StoredProcedure))
                        .ToList();

                logger.LogInformation(
                    "{Announcement}: Attempt to retrieve all persons completed successfully",
                    "SUCCEEDED");
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to retrieve all persons was unsuccessful",
                    "FAILED");
            }

            return persons;
        }

        public async Task<PersonModel?> RetrieveSingleAsync(int personId)
        {
            logger.LogInformation(
            "Repository => Attempting to retrieve person {Person}",
            personId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            PersonModel? person = null;

            try
            {
                person =
                    await sqlConnection.QuerySingleOrDefaultAsync<PersonModel>(
                        sql: storedProcedures.Value.GetPersonById,
                        param: new { personId },
                        commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to retrieve person {Person} completed successfully",
                    "SUCCEEDED", personId);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to retrieve person {Person} was unsuccessful",
                    "FAILED", personId);

                person = null;
            }

            return person;
        }


        public async Task<bool> UpdateAsync(int personId, PersonModel person)
        {
            logger.LogInformation(
             "Repository => Attempting to update person {Person}",
             personId);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.UpdatePersonById,
                    param: new
                    {
                        personId,
                        person.Name,
                        person.Surname,
                        person.IdNumber
                    },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to update person {Person} completed successfully",
                    "SUCCEEDED", personId);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to update person {Person} was unsuccessful",
                    "FAILED", personId);

                return false;
            }
        }
    }


}

