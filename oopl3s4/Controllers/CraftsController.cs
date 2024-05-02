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
    public class CraftsController : Controller
    {
        private readonly MyDbContext _context;

        public CraftsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Crafts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Craft.ToListAsync());
        }

        // GET: Crafts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await _context.Craft
                .FirstOrDefaultAsync(m => m.Id == id);
            if (craft == null)
            {
                return NotFound();
            }

            return View(craft);
        }

        // GET: Crafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Crafts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Craft craft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(craft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(craft);
        }

        // GET: Crafts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await _context.Craft.FindAsync(id);
            if (craft == null)
            {
                return NotFound();
            }
            return View(craft);
        }

        // POST: Crafts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Craft craft)
        {
            if (id != craft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(craft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CraftExists(craft.Id))
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
            return View(craft);
        }

        // GET: Crafts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var craft = await _context.Craft
                .FirstOrDefaultAsync(m => m.Id == id);
            if (craft == null)
            {
                return NotFound();
            }

            return View(craft);
        }

        // POST: Crafts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var craft = await _context.Craft.FindAsync(id);
            if (craft != null)
            {
                _context.Craft.Remove(craft);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CraftExists(int id)
        {
            return _context.Craft.Any(e => e.Id == id);
        }
    }
}
