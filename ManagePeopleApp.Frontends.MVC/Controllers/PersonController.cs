using ManagePeople.Libraries.Shared;
using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ManagePeopleApp.Frontends.MVC.Controllers
{
    public class PersonController
        (IPersonClientService _personClientService,
    ILogger<PersonController> _logger) : Controller
     {
        public async Task<IActionResult> Index()
        {
            var persons = await _personClientService.GetPersonsAsync();
            return View(persons);
        }


        public IActionResult Create()
        {
            return View();
        }

        // POST: Persons/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonModel person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var createdPerson = await _personClientService.CreatePersonAsync(person);
            if (createdPerson == null)
            {
                TempData["ErrorMessage"] = "Failed to create the person.";
                return View(person);
            }

            TempData["SuccessMessage"] = "Person created successfully.";
            return RedirectToAction(nameof(Index));
        }

        // GET: Persons/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personClientService.GetPersonByPersonIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Persons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonModel person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var updated = await _personClientService.UpdatePersonAsync(person);
            if (!updated)
            {
                TempData["ErrorMessage"] = "Failed to update the person.";
                return View(person);
            }

            TempData["SuccessMessage"] = "Person updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        // POST: Persons/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _personClientService.DeletePersonAsync(id);
            if (!deleted)
            {
                return BadRequest(new { message = "Failed to delete the person." });
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
