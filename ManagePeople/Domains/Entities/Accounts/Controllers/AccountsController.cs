using ManagePeople.Domains.Entities.Accounts.Repositories;
using ManagePeople.Domains.Entities.Persons.Controllers;
using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Libraries.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

namespace ManagePeople.Domains.Entities.Accounts.Controllers
{
    [Route("api/accounts/")]
    [ApiController]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class AccountsController(ILogger<AccountsController> logger,
    IAccountsRepository accountsRepository,
    IPersonsRepository personsRepository) : Controller
    {
        [HttpPost]
        [ProducesResponseType<AccountModel>(StatusCodes.Status201Created)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AccountModel>> CreateAsync(int personId, AccountModel accountModel)
        {
            logger.LogInformation(
                "Controller => Attempting to add a account to person {Person}",
                personId);

            if (personId != accountModel.PersonId)
            {
                logger.LogWarning(
                    "{Announcement}: A user attempted to create a new account for a different person",
                    "WARNING");

                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var person = await personsRepository.RetrieveSingleAsync(personId);

            if (person is null)
            {
                logger.LogWarning(
                    "{Announcement}: The requested person was not found",
                    "WARNING");

                return NotFound();
            }

            var createdAccount = await accountsRepository.CreateAsync(personId, accountModel);

            if (createdAccount is null)
            {
                return BadRequest();
            }


            return CreatedAtRoute(
                routeName: nameof(RetrieveSingleAccountAsync),
                routeValues: new { personId, accountId = createdAccount.AccountId },
                value: createdAccount);
        }

        [HttpGet]
        [ProducesResponseType<List<AccountModel>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccountModel>>> RetrieveAllAsync(int personId)
        {
            logger.LogInformation(
                "Controller => Attempting to retrieve all accounts for person {Person}",
                personId);

            var accounts = await accountsRepository.RetrieveAllAsync(personId);

            return Ok(accounts);
        }

        [HttpGet("{accountId:int}", Name = nameof(RetrieveSingleAccountAsync))]
        [ProducesResponseType<AccountModel>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonModel>> RetrieveSingleAccountAsync(int accountId)
        {
            logger.LogInformation(
                "Controller => Attempting to retrieve account {Account}",
                accountId);

            var person = await accountsRepository.RetrieveSingleAsync(accountId);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPut("{accountId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int personId, int accountId, AccountModel accountModel)
        {
            logger.LogInformation(
                "Controller => Attempting to update account {Account} from person {Person}",
                accountId, personId);

            var updateIsAllowed = personId == accountModel.PersonId && accountId == accountModel.AccountId;

            if (!updateIsAllowed)
            {
                logger.LogWarning(
                    "{Announcement}: A user attempted to either update a different account from the same person or a different account from a different person",
                    "WARNING");

                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var person = await personsRepository.RetrieveSingleAsync(personId);

            if (person is null)
            {
                logger.LogWarning(
                    "{Announcement}: Person {Person} does not exist",
                    "WARNING", personId);

                return NotFound();
            }

            var account = await accountsRepository.RetrieveSingleAsync(accountId);

            if (account is null)
            {
                logger.LogWarning(
                    "{Announcement}: Account {Account} from person {Person} does not exist",
                    "WARNING", accountId, personId);

                return NotFound();
            }

            var updateSucceeded = await accountsRepository.UpdateAsync(personId, accountId, accountModel);


            return updateSucceeded ? NoContent() : BadRequest();
        }

        [HttpDelete("{accountId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int accountId)
        {
            logger.LogInformation(
                "Controller => Attempting to delete account {AccountId}",
                accountId);

            var account = await accountsRepository.RetrieveSingleAsync(accountId);

            if (account is null)
            {
                logger.LogWarning(
                    "{Announcement}: The account [{Account}] that was requested for deletion was not found",
                    "WARNING", accountId);

                return NotFound();
            }

            var deleteSucceeded = await accountsRepository.DeleteAsync(accountId);

            return deleteSucceeded ? NoContent() : BadRequest();
        }
    }


    
}
