using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicDemoWebsite.Context;
using MusicDemoWebsite.Models;

namespace MusicDemoWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsApiController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public ArtistsApiController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: api/ArtistsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.Include(x => x.Songs).ToListAsync();
        }

        // GET: api/ArtistsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(Guid id)
        {
            var artist = await _context.Artists.FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            return artist;
        }

        // PUT: api/ArtistsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(Guid id, Artist artist)
        {
            if (id != artist.ArtistID)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ArtistsApi
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.ArtistID }, artist);
        }

        // DELETE: api/ArtistsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(Guid id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return artist;
        }

        private bool ArtistExists(Guid id)
        {
            return _context.Artists.Any(e => e.ArtistID == id);
        }
    }
}
