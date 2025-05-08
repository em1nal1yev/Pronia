using Microsoft.AspNetCore.Mvc;

namespace ProniaOneToManyFileCRUD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
