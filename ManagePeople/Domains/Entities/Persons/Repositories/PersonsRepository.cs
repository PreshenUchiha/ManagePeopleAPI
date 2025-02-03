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

            dynamicParams.Add(name: "@Code", dbType: DbType.Int32, direction: ParameterDirection.Output);
            dynamicParams.Add(name: "@Name", value: person.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@Surname", value: person.Surname, dbType: DbType.String, direction: ParameterDirection.Input);
            dynamicParams.Add(name: "@Id_Number", value: person.IdNumber, dbType: DbType.String, direction: ParameterDirection.Input);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.InsertNewPerson,
                    param: dynamicParams,
                    commandType: CommandType.StoredProcedure);

                person.Code = dynamicParams.Get<int>("@Code");

                logger.LogInformation(
                    "{Announcement}: Attempt to create a new person completed successfully with id {Person}",
                    "SUCCEEDED", person.Code);

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

        public async Task<bool> DeleteAsync(int code)
        {
            logger.LogInformation(
    "Repository => Attempting to delete team {Team}",
         code);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.DeletePersonById,
                    param: new { Code=code },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to delete team {Team} completed successfully",
                    "SUCCEEDED", code);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to delete team {Team} was unsuccessful",
                    "FAILED", code);

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

        public async Task<PersonModel?> RetrieveSingleAsync(int code)
        {
            logger.LogInformation(
            "Repository => Attempting to retrieve person {Person}",
            code);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            PersonModel? person = null;

            try
            {
                person =
                    await sqlConnection.QuerySingleOrDefaultAsync<PersonModel>(
                        sql: storedProcedures.Value.GetPersonById,
                        param: new { code },
                        commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to retrieve person {Person} completed successfully",
                    "SUCCEEDED", code);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to retrieve person {Person} was unsuccessful",
                    "FAILED", code);

                person = null;
            }

            return person;
        }


        public async Task<bool> UpdateAsync(int code, PersonModel person)
        {
            logger.LogInformation(
             "Repository => Attempting to update team {Team}",
             code);

            using var sqlConnection = new SqlConnection(connectionStrings.Value.ManagePeopleDb);

            try
            {
                await sqlConnection.ExecuteAsync(
                    sql: storedProcedures.Value.UpdatePersonById,
                    param: new
                    {
                        code,
                        person.Name,
                        person.Surname,
                        Id_Number = person.IdNumber
                    },
                    commandType: CommandType.StoredProcedure);

                logger.LogInformation(
                    "{Announcement}: Attempt to update person {Person} completed successfully",
                    "SUCCEEDED", code);

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "{Announcement}: Attempt to update person {Person} was unsuccessful",
                    "FAILED", code);

                return false;
            }
        }
    }


}

