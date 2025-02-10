using ManagePeople.Libraries.Shared;
using ManagePeopleApp.Frontends.MVC.Domains.Accounts.Interface;
using ManagePeopleApp.Frontends.MVC.Domains.Persons.Interface;
using ManagePeopleApp.Frontends.MVC.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagePeopleApp.Frontends.MVC.Controllers
{
    public class AccountController(
        IAccountClientService accountClientService,
        IPersonClientService personClientService,
        ILogger<AccountController> logger) : Controller
    {
        // GET: AccountController
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

           // HttpContext.Session.SetString(Constants.SessionKey, id.ToString());

            ViewBag.DisableButton = "";
            ViewBag.Redirect = false;

            
            var person = await personClientService.GetPersonByPersonIdAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return View(new AccountModel { PersonId = id });
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
