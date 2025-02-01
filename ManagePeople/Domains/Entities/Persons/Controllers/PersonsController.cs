using Microsoft.AspNetCore.Mvc;

namespace ManagePeople.Domains.Entities.Persons.Controllers
{
    public class PersonsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
