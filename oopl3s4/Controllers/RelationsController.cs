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
    public class RelationsController : Controller
    {
        private readonly MyDbContext _context;

        public RelationsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Relations
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Relation.Include(r => r.Artisan).Include(r => r.Craft);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Relations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relation
                .Include(r => r.Artisan)
                .Include(r => r.Craft)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relation == null)
            {
                return NotFound();
            }

            return View(relation);
        }

        // GET: Relations/Create
        public IActionResult Create()
        {
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id");
            ViewData["CraftID"] = new SelectList(_context.Craft, "Id", "Id");
            return View();
        }

        // POST: Relations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ArtisanID,CraftID")] Relation relation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(relation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", relation.ArtisanID);
            ViewData["CraftID"] = new SelectList(_context.Craft, "Id", "Id", relation.CraftID);
            return View(relation);
        }

        // GET: Relations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relation.FindAsync(id);
            if (relation == null)
            {
                return NotFound();
            }
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", relation.ArtisanID);
            ViewData["CraftID"] = new SelectList(_context.Craft, "Id", "Id", relation.CraftID);
            return View(relation);
        }

        // POST: Relations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArtisanID,CraftID")] Relation relation)
        {
            if (id != relation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(relation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RelationExists(relation.Id))
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
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", relation.ArtisanID);
            ViewData["CraftID"] = new SelectList(_context.Craft, "Id", "Id", relation.CraftID);
            return View(relation);
        }

        // GET: Relations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var relation = await _context.Relation
                .Include(r => r.Artisan)
                .Include(r => r.Craft)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (relation == null)
            {
                return NotFound();
            }

            return View(relation);
        }

        // POST: Relations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var relation = await _context.Relation.FindAsync(id);
            if (relation != null)
            {
                _context.Relation.Remove(relation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RelationExists(int id)
        {
            return _context.Relation.Any(e => e.Id == id);
        }
    }
}
