using ManagePeople.Domains.Entities.Persons.Repositories;
using ManagePeople.Libraries.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
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
        [HttpGet("{personId:int}", Name = nameof(RetrieveSinglePersonAsync))]
        [ProducesResponseType<PersonModel>(StatusCodes.Status200OK)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonModel>> RetrieveSinglePersonAsync(int personId)
        {
            logger.LogInformation(
                "Controller => Attempting to retrieve person {Person}",
                personId);

            var person = await personsRepository.RetrieveSingleAsync(personId);

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
                routeValues: new { personId = createdPerson.PersonId },
                value: createdPerson);
        }

        [HttpDelete("{personId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int personId)
        {
            logger.LogInformation(
                "Controller => Attempting to delete person {PersonId}",
                personId);

            var person = await personsRepository.RetrieveSingleAsync(personId);

            if (person is null)
            {
                logger.LogWarning(
                    "{Announcement}: The person [{Person}] that was requested for deletion was not found",
                    "WARNING", personId);

                return NotFound();
            }

            var deleteSucceeded = await personsRepository.DeleteAsync(personId);

            return deleteSucceeded ? NoContent() : BadRequest();
        }

        [HttpPut("{personId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status403Forbidden)]
        [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync(int personId, PersonModel person)
        {
            logger.LogInformation(
                "Controller => Attempting to update person {Person}",
                personId);

            if (person.PersonId != personId)
            {
                logger.LogWarning(
                    "{Announcement}: Someone attempted to modify another person's definition",
                    "WARNING");

                return StatusCode(StatusCodes.Status403Forbidden);
            }

            var existingTeam = await personsRepository.RetrieveSingleAsync(personId);

            if (existingTeam is null)
            {
                logger.LogWarning(
                    "{Announcement}: Person {Person} was not found",
                    "WARNING", personId);

                return NotFound();
            }

            var updateSucceeded = await personsRepository.UpdateAsync(personId, person);


            return updateSucceeded ? NoContent() : BadRequest();
        }

    }
}
