using FinallyProject.DAL;
using FinallyProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinallyProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ServiceController : Controller
    { private readonly AppDbContext _context;
        public ServiceController(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.Include(e => e.Service).ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {

            ViewBag.Services = await _context.Services.ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product )
        {

            bool result = await _context.Services.AnyAsync(p => p.Id == product.ServiceId);
            if (!result)
            {
                ModelState.AddModelError("PositionId", "There is no position with this Id");
                ViewBag.Services = await _context.Services.ToListAsync();
                return View();
            }
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();

            ViewBag.Services = await _context.Services.ToListAsync();

            return View(existed);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Product product)
        {

            if (id == null || id < 1) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Services = await _context.Services.ToListAsync();
                return View(existed);
            }

            if (existed.ServiceId!=product.ServiceId)
            {
                bool result = await _context.Services.AnyAsync(p => p.Id == product.ServiceId);
                if (!result)
                {
                    ModelState.AddModelError("ServicesId", "Not Found");
                    ViewBag.Services = await _context.Services.ToListAsync();

                    return View(existed);
                }
                product.ServiceId = product.ServiceId;
            }


            existed.Title = product.Title;


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(s => s.Id == id);

            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
