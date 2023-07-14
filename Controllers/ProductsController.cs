using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoutiqueProje.Data;
using BoutiqueProje.Models;

namespace BoutiqueProje.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BoutiqueProductContext _context;

        public ProductsController(BoutiqueProductContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var boutiqueProductContext = _context.Products.Include(p => p.Category);

            var lst_product = from p in _context.Products
                             .Include(p => p.Category)
                             .Include(p => p.ProdColors)                             
                              select new ProductViewModel()
                              {
                                  Id = p.Id,
                                  ProductName = p.ProductName,
                                  Color = p.Color,
                                  Size = p.SizeId,
                                  CategoryId = p.Category.Id,
                                  ImageName= p.ImageName

                              };

            return View(await lst_product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p=>p.ProductName)
                .Include(p=>p.SizeId)
                .Include(p=>p.ColorCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["SizeId"] = new SelectList(new int[] { 34, 36, 38, 40, 42, 44 });
            ViewData["ColorCode"] = new SelectList(_context.ProdColors, "Id", "Color");
            ViewData["ProductCode"] = new SelectList(_context.Products, "Id", "ProductName");
            ViewData["ImageId"] = new SelectList(_context.Products, "Id", "ImageName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Size,Color,Id,Name,Description,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SizeId"] = new SelectList(new int[] { 34, 36, 38, 40, 42, 44 });
            ViewData["ColorCode"] = new SelectList(_context.ProdColors, "Id","Name", product.ColorCode);
            ViewData["ImageName"] = new SelectList("Id", "Name", product.ImageName);


            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SizeId"] = new SelectList(new int[] { 34, 36, 38, 40, 42, 44 });
            ViewData["ColorCode"] = new SelectList(_context.ProdColors, "Id", "Name", product.ColorCode);

            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,Size,Color,Id,Name,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewData["SizeId"] = new SelectList(new int[] { 34, 36, 38, 40, 42, 44 });
            ViewData["ColorCode"] = new SelectList(_context.ProdColors, "Id", "Name", product.ColorCode);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
               .Include(p => p.Category)
                .Include(p => p.ProductName)
                .Include(p => p.SizeId)
                .Include(p => p.ColorCode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'BoutiqueProductContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
