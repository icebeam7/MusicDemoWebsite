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
    public class SongsApiController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public SongsApiController(RepositoryContext context)
        {
            _context = context;
        }

        // GET: api/SongsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        // GET: api/SongsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/SongsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(Guid id, Song song)
        {
            if (id != song.SongID)
            {
                return BadRequest();
            }

            _context.Entry(song).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/SongsApi
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSong", new { id = song.SongID }, song);
        }

        // DELETE: api/SongsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> DeleteSong(Guid id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return song;
        }

        private bool SongExists(Guid id)
        {
            return _context.Songs.Any(e => e.SongID == id);
        }
    }
}
