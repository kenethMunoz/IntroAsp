using IntroAsp.Models;
using IntroAsp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IntroAsp.Controllers
{
    public class BeerController : Controller
    {
        private readonly PubContext _context;
        public BeerController(PubContext context) {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var beers = await _context.Beers.Include(b => b.ID_BrandNavigation).OrderBy(b => b.Name).ToListAsync();
            return View(beers);
        }

        public IActionResult Create() {
            ViewData["Brands"] = new SelectList(_context.Brands, "ID_Brand", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BeerViewModel model)
            
        {
            if (ModelState.IsValid)
            {
                var beer = new Beer()
                {
                    Name = model.Name,
                    ID_Brand = model.ID_Brand
                };
                _context.Add(beer);
                await _context.SaveChangesAsync(); 
                return RedirectToAction(nameof(Index));
            }
            else { 

            
            }
            ViewData["Brands"] = new SelectList(_context.Brands, "ID_Brand", "Name", model.ID_Brand);
            return View(model);
        }
    }
}
