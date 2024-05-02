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
    public class EmailsController : Controller
    {
        private readonly MyDbContext _context;

        public EmailsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Emails
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Emails.Include(e => e.Artisan);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Emails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emails = await _context.Emails
                .Include(e => e.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emails == null)
            {
                return NotFound();
            }

            return View(emails);
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,ArtisanID")] Emails emails)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(emails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", emails.ArtisanID);
            return View(emails);
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emails = await _context.Emails.FindAsync(id);
            if (emails == null)
            {
                return NotFound();
            }
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", emails.ArtisanID);
            return View(emails);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,ArtisanID")] Emails emails)
        {
            if (id != emails.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailsExists(emails.Id))
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
            ViewData["ArtisanID"] = new SelectList(_context.Artisan, "Id", "Id", emails.ArtisanID);
            return View(emails);
        }

        // GET: Emails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emails = await _context.Emails
                .Include(e => e.Artisan)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emails == null)
            {
                return NotFound();
            }

            return View(emails);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emails = await _context.Emails.FindAsync(id);
            if (emails != null)
            {
                _context.Emails.Remove(emails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailsExists(int id)
        {
            return _context.Emails.Any(e => e.Id == id);
        }
    }
}
