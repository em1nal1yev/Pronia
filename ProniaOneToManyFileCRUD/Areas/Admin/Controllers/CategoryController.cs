using Microsoft.AspNetCore.Mvc;
using ProniaOneToManyFileCRUD.DAL;
using ProniaOneToManyFileCRUD.Models;

namespace ProniaOneToManyFileCRUD.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category newCategory)
        {
            if (!ModelState.IsValid) { return BadRequest(); }

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync(); 
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Category deletedCategory = _context.Categories.FirstOrDefault(x => x.Id == id);
            _context.Categories.Remove(deletedCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Category updatedCategory)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            _context.Categories.Update(updatedCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
