using Microsoft.AspNetCore.Mvc;

namespace ManagePeople.Domains.Entities.Accounts.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
