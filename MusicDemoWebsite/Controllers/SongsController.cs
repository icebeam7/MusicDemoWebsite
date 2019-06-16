using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicDemoWebsite.Context;
using MusicDemoWebsite.Models;

namespace MusicDemoWebsite.Controllers
{
    public class SongsController : Controller
    {
        private readonly RepositoryContext _context;

        public SongsController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index()
        {
            var repositoryContext = _context.Songs.Include(s => s.Artist);
            return View(await repositoryContext.ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "ArtistName");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SongID,SongName,ArtistID")] Song song)
        {
            if (ModelState.IsValid)
            {
                song.SongID = Guid.NewGuid();
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "ArtistName", song.ArtistID);
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "ArtistName", song.ArtistID);
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SongID,SongName,ArtistID")] Song song)
        {
            if (id != song.SongID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.SongID))
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
            ViewData["ArtistID"] = new SelectList(_context.Artists, "ArtistID", "ArtistName", song.ArtistID);
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.SongID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(Guid id)
        {
            return _context.Songs.Any(e => e.SongID == id);
        }
    }
}
