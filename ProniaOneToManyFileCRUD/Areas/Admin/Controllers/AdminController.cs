using Microsoft.AspNetCore.Mvc;
using ProniaOneToManyFileCRUD.DAL;

namespace ProniaOneToManyFileCRUD.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        public IActionResult Index()
        {

            return View();
        }
    }
}
