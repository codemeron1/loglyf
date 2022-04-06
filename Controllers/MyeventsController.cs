#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LogLife11.Data;
using LogLife11.Models;
using Microsoft.AspNetCore.Authorization;

namespace LogLife11.Controllers
{
    public class MyeventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyeventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Myevents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Myevent.ToListAsync());
        }

        // GET: Myevents/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Myevents/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Myevent.Where( j => j.NameEvent.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Myevents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myevent = await _context.Myevent
                .FirstOrDefaultAsync(m => m.id == id);
            if (myevent == null)
            {
                return NotFound();
            }

            return View(myevent);
        }

        // GET: Myevents/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Myevents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,NameEvent,NameOrganizer")] Myevent myevent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myevent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(myevent);
        }
        [Authorize]
        // GET: Myevents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myevent = await _context.Myevent.FindAsync(id);
            if (myevent == null)
            {
                return NotFound();
            }
            return View(myevent);
        }

        // POST: Myevents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,NameEvent,NameOrganizer")] Myevent myevent)
        {
            if (id != myevent.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myevent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyeventExists(myevent.id))
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
            return View(myevent);
        }

        // GET: Myevents/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var myevent = await _context.Myevent
                .FirstOrDefaultAsync(m => m.id == id);
            if (myevent == null)
            {
                return NotFound();
            }

            return View(myevent);
        }

        // POST: Myevents/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var myevent = await _context.Myevent.FindAsync(id);
            _context.Myevent.Remove(myevent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyeventExists(int id)
        {
            return _context.Myevent.Any(e => e.id == id);
        }
    }
}
