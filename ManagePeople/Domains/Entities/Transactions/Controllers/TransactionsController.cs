using Dapper;
using ManagePeople.Configuration;
using ManagePeople.Domains.Entities.Accounts.Controllers;
using ManagePeople.Domains.Entities.Accounts.Repositories;
using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Domains.Entities.Transactions.Repositories;
using ManagePeople.Libraries.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace ManagePeople.Domains.Entities.Transactions.Controllers
{
    [Route("api/transactions/")]
    [ApiController]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class TransactionsController(
    ILogger<TransactionsController> logger,
    ITransactionsRepository transactionsRepository,
    IAccountsRepository accountsRepository) : Controller
    {
        [HttpPost]
        [ProducesResponseType<TransactionModel>(StatusCodes.Status201Created)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionModel>> CreateAsync(int accountId, TransactionModel transactionModel)
        {
            logger.LogInformation(
                "Controller => Attempting to add a transaction to account {Account}",
                accountId);

            if (accountId != transactionModel.AccountId)
            {
                logger.LogWarning(
                    "{Announcement}: A user attempted to create a new transaction for a different account",
                    "WARNING");

                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var account = await accountsRepository.RetrieveSingleAsync(accountId);

            if (account is null)
            {
                logger.LogWarning(
                    "{Announcement}: The requested account was not found",
                    "WARNING");

                return NotFound();
            }

            var createdTransaction = await transactionsRepository.CreateAsync(accountId, transactionModel);

            if (createdTransaction is null)
            {
                return BadRequest();
            }


            return CreatedAtRoute(
                routeName: nameof(RetrieveSingleTransactionAsync),
                routeValues: new { accountId, transactionId = createdTransaction.TransactionId },
                value: createdTransaction);
        }

        [HttpGet]
        [ProducesResponseType<List<TransactionModel>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TransactionModel>>> RetrieveAllAsync(int accountId)
        {
            logger.LogInformation(
                "Controller => Attempting to retrieve all transactions for account {Account}",
                accountId);

            var transactions = await transactionsRepository.RetrieveAllAsync(accountId);

            return Ok(transactions);
        }

        [HttpGet("{transactionId:int}", Name = nameof(RetrieveSingleTransactionAsync))]
        [ProducesResponseType<TransactionModel>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionModel>> RetrieveSingleTransactionAsync(int transactionId)
        {
            logger.LogInformation(
                "Controller => Attempting to retrieve transaction {Transaction}",
                transactionId);

            var transaction = await transactionsRepository.RetrieveSingleAsync(transactionId);

            if (transaction is null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        [HttpPut("{transactionId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int accountId, int transactionId, TransactionModel transactionModel)
        {
            logger.LogInformation(
                "Controller => Attempting to update transaction {Transaction} from account {Account}",
                transactionId, accountId);

            var updateIsAllowed = accountId == transactionModel.AccountId && transactionId == transactionModel.TransactionId;

            if (!updateIsAllowed)
            {
                logger.LogWarning(
                    "{Announcement}: A user attempted to either update a different transaction from the same account or a different transaction from a different account",
                    "WARNING");

                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var account = await accountsRepository.RetrieveSingleAsync(accountId);

            if (account is null)
            {
                logger.LogWarning(
                    "{Announcement}: Account {Account} does not exist",
                    "WARNING", accountId);

                return NotFound();
            }

            var transaction = await transactionsRepository.RetrieveSingleAsync(transactionId);

            if (transaction is null)
            {
                logger.LogWarning(
                    "{Announcement}: Transaction {Transaction} from account {Account} does not exist",
                    "WARNING", transactionId, accountId);

                return NotFound();
            }

            var updateSucceeded = await transactionsRepository.UpdateAsync(accountId, transactionId, transactionModel);


            return updateSucceeded ? NoContent() : BadRequest();
        }


        [HttpDelete("{transactionId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int transactionId)
        {
            logger.LogInformation(
                "Controller => Attempting to delete account {Transaction}",
                transactionId);

            var transaction = await transactionsRepository.RetrieveSingleAsync(transactionId);

            if (transaction is null)
            {
                logger.LogWarning(
                    "{Announcement}: The transaction [{Transaction}] that was requested for deletion was not found",
                    "WARNING", transactionId);

                return NotFound();
            }

            var deleteSucceeded = await transactionsRepository.DeleteAsync(transactionId);

            return deleteSucceeded ? NoContent() : BadRequest();
        }

    }
}
