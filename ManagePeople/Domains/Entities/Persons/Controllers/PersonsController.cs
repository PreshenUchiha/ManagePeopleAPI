using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Libraries.Shared;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace ManagePeople.Domains.Entities.Persons.Controllers
{
    [Route("api/persons")]
    [ApiController]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class PersonsController(ILogger<PersonsController> logger,
    IPersonsRepository personsRepository) : Controller
    {
        [HttpGet("{code:int}", Name = nameof(RetrieveSinglePersonAsync))]
        [ProducesResponseType<PersonModel>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonModel>> RetrieveSinglePersonAsync(int code)
        {
            logger.LogInformation(
                "Controller => Attempting to retrieve person {Person}",
                code);

            var person = await personsRepository.RetrieveSingleAsync(code);

            if (person is null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpGet]
        [ProducesResponseType<List<PersonModel>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PersonModel>>> RetrieveAllAsync()
        {

            logger.LogInformation("Controller => Attempting to retrieve all teams");

            var persons = await personsRepository.RetrieveAllAsync();

            return Ok(persons);
        }

        [HttpPost]
        [ProducesResponseType<PersonModel>(StatusCodes.Status201Created)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonModel>> CreateAsync(PersonModel person)
        {
            logger.LogInformation("Controller => Attempting to create a new person");

            var createdPerson = await personsRepository.CreateAsync(person);

            if (createdPerson is null)
            {
                return BadRequest();
            }

            return CreatedAtRoute(
                routeName: nameof(RetrieveSinglePersonAsync),
                routeValues: new { code = createdPerson.Code },
                value: createdPerson);
        }

        [HttpDelete("{code:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int code)
        {
            logger.LogInformation(
                "Controller => Attempting to delete team {Code}",
                code);

            var person = await personsRepository.RetrieveSingleAsync(code);

            if (person is null)
            {
                logger.LogWarning(
                    "{Announcement}: The person [{Person}] that was requested for deletion was not found",
                    "WARNING", code);

                return NotFound();
            }

            var deleteSucceeded = await personsRepository.DeleteAsync(code);

            return deleteSucceeded ? NoContent() : BadRequest();
        }
    }
}
