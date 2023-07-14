using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoutiqueProje.Data;

namespace BoutiqueProje.Controllers
{
    public class ProdColorsController : Controller
    {
        private readonly BoutiqueProductContext _context;

        public ProdColorsController(BoutiqueProductContext context)
        {
            _context = context;
        }

        // GET: ProdColors
        public async Task<IActionResult> Index()
        {
              return View(await _context.ProdColors.ToListAsync());
        }

        // GET: ProdColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProdColors == null)
            {
                return NotFound();
            }

            var prodColor = await _context.ProdColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodColor == null)
            {
                return NotFound();
            }

            return View(prodColor);
        }

        // GET: ProdColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProdColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ColorCode,Color,Id,Name,Description")] ProdColor prodColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prodColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodColor);
        }

        // GET: ProdColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProdColors == null)
            {
                return NotFound();
            }

            var prodColor = await _context.ProdColors.FindAsync(id);
            if (prodColor == null)
            {
                return NotFound();
            }
            return View(prodColor);
        }

        // POST: ProdColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ColorCode,Color,Id,Name,Description")] ProdColor prodColor)
        {
            if (id != prodColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdColorExists(prodColor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(prodColor);
        }

        // GET: ProdColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProdColors == null)
            {
                return NotFound();
            }

            var prodColor = await _context.ProdColors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prodColor == null)
            {
                return NotFound();
            }

            return View(prodColor);
        }

        // POST: ProdColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProdColors == null)
            {
                return Problem("Entity set 'BoutiqueProductContext.ProdColors'  is null.");
            }
            var prodColor = await _context.ProdColors.FindAsync(id);
            if (prodColor != null)
            {
                _context.ProdColors.Remove(prodColor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdColorExists(int id)
        {
          return _context.ProdColors.Any(e => e.Id == id);
        }
    }
}
