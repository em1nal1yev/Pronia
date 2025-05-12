using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProniaOneToManyFileCRUD.DAL;
using ProniaOneToManyFileCRUD.Helper;
using ProniaOneToManyFileCRUD.Models;

namespace ProniaOneToManyFileCRUD.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            if (!product.File.ContentType.Contains("image"))
            {
                return BadRequest();
            }
            string fileName = product.File.CreateFile(_webHostEnvironment.WebRootPath, "Uploads/Products");
            product.ImgUrl = fileName;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Product deletedProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads/Products", deletedProduct.ImgUrl);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Products.Remove(deletedProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Product updateNeededProduct = await _context.Products.Include(x=>x.Category).FirstOrDefaultAsync(p => p.Id == id);
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View((updateNeededProduct));
        }
        [HttpPost]
        public async Task<IActionResult> Update( Product updatedProduct)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var existingProduct = await _context.Products.FindAsync(updatedProduct.Id);
            if(updatedProduct.File != null && updatedProduct.File.ContentType.Contains("image"))
            {
                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "Upload/Product", existingProduct.ImgUrl);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

            }
            string newFileName = updatedProduct.File.CreateFile(_webHostEnvironment.WebRootPath, "Uploads/Products");
            existingProduct.ImgUrl = newFileName;
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.CategoryId = updatedProduct.CategoryId;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}
