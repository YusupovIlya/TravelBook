using Microsoft.AspNetCore.Mvc;

namespace TravelBook.Web.Areas.Account.Controllers
{
    public class Account : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
