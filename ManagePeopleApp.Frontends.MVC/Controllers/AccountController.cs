using ManagePeopleApp.Frontends.MVC.Domains.Accounts.Interface;
using ManagePeopleApp.Frontends.MVC.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagePeopleApp.Frontends.MVC.Controllers
{
    public class AccountController(
        IAccountClientService accountClientService,
        ILogger<AccountController> logger) : Controller
    {
        // GET: AccountController
        [HttpGet("~/Teams/{id:int}/Members")]
        public async Task<IActionResult> GetAccountsByPersonId(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            HttpContext.Session.SetString(Constants.SessionKey, id.ToString());

            ViewBag.DisableButton = "";
            ViewBag.Redirect = false;

            
            var team = await teamClientService.GetTeamByTeamIdAsync(id);

            if (User.IsInRole(Constants.TeamViewer) && !signedInUsername.Equals(team?.TeamLeadActiveDirectoryUsername, StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.DisableButton = "disabled";
                ViewBag.Redirect = true;

                return View(new ExtendedTeamMemberModel { TeamId = id });
            }

            if (team is null)
            {
                return NotFound();
            }

            return View(new ExtendedTeamMemberModel { TeamId = id });
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
