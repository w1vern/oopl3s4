using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using oopl3s4.Data;
using oopl3s4.Models;

namespace oopl3s4.Controllers
{
    public class IsInheritancesController : Controller
    {
        private readonly MyDbContext _context;

        public IsInheritancesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: IsInheritances
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.IsInheritance.Include(i => i.Artisan);
            return View(await myDbContext.ToListAsync());
        }

        // GET: IsInheritances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInheritance = await _context.IsInheritance
                .Include(i => i.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (isInheritance == null)
            {
                return NotFound();
            }

            return View(isInheritance);
        }

        // GET: IsInheritances/Create
        public IActionResult Create()
        {
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id");
            return View();
        }

        // POST: IsInheritances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtisanID,isInh")] IsInheritance isInheritance)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(isInheritance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", isInheritance.ArtisanID);
            return View(isInheritance);
        }

        // GET: IsInheritances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInheritance = await _context.IsInheritance.FindAsync(id);
            if (isInheritance == null)
            {
                return NotFound();
            }
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", isInheritance.ArtisanID);
            return View(isInheritance);
        }

        // POST: IsInheritances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArtisanID,isInh")] IsInheritance isInheritance)
        {
            if (id != isInheritance.Id)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isInheritance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsInheritanceExists(isInheritance.Id))
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
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", isInheritance.ArtisanID);
            return View(isInheritance);
        }

        // GET: IsInheritances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isInheritance = await _context.IsInheritance
                .Include(i => i.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (isInheritance == null)
            {
                return NotFound();
            }

            return View(isInheritance);
        }

        // POST: IsInheritances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isInheritance = await _context.IsInheritance.FindAsync(id);
            if (isInheritance != null)
            {
                _context.IsInheritance.Remove(isInheritance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsInheritanceExists(int id)
        {
            return _context.IsInheritance.Any(e => e.Id == id);
        }
    }
}
