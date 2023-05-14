using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToMany.Data;
using OneToMany.Models;

namespace OneToMany.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.ProductImage).Where(m => !m.SoftDeleted).Take(4).ToListAsync();
            int count = await _context.Products.Where(m => !m.SoftDeleted).CountAsync();
            ViewBag.productCount = count;
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> ShowMoreOrLess(int skip)
        {
            IEnumerable<Product> products = await _context.Products.Include(m => m.ProductImage).Where(m => !m.SoftDeleted).Skip(skip).Take(4).ToListAsync();

            return PartialView("_ProductsPartial",products);
        }
    }
}
