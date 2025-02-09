using ManagePeople.Libraries.Shared;
using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ManagePeopleApp.Frontends.MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPersonClientService _personClientService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonClientService personClientService, ILogger<PersonController> logger)
        {
            _personClientService = personClientService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var persons = await _personClientService.GetPersonsAsync();
            return View(persons);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonModel person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            await _personClientService.CreatePersonAsync(person);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personClientService.GetPersonByPersonIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PersonModel person)
        {
            if (id != person.PersonId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(person);
            }

            await _personClientService.UpdatePersonAsync(person);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personClientService.GetPersonByPersonIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personClientService.DeletePersonAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var person = await _personClientService.GetPersonByPersonIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
    }
}
