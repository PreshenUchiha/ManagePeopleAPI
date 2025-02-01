using Microsoft.AspNetCore.Mvc;

namespace ManagePeople.Domains.Entities.Transactions.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
