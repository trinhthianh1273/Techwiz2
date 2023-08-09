using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerImagesController : ControllerBase
    {
        private readonly SoccerContext _context;

        public PlayerImagesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/PlayerImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerImage>>> GetPlayerImage()
        {
          if (_context.PlayerImage == null)
          {
              return NotFound();
          }
            return await _context.PlayerImage.ToListAsync();
        }

        // GET: api/PlayerImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerImage>> GetPlayerImage(int id)
        {
          if (_context.PlayerImage == null)
          {
              return NotFound();
          }
            var playerImage = await _context.PlayerImage.FindAsync(id);

            if (playerImage == null)
            {
                return NotFound();
            }

            return playerImage;
        }

        // PUT: api/PlayerImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerImage(int id, PlayerImage playerImage)
        {
            if (id != playerImage.PlayerImageID)
            {
                return BadRequest();
            }

            _context.Entry(playerImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerImageExists(id))
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

        // POST: api/PlayerImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerImage>> PostPlayerImage(PlayerImage playerImage)
        {
          if (_context.PlayerImage == null)
          {
              return Problem("Entity set 'SoccerContext.PlayerImage'  is null.");
          }
            _context.PlayerImage.Add(playerImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerImage", new { id = playerImage.PlayerImageID }, playerImage);
        }

        // DELETE: api/PlayerImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerImage(int id)
        {
            if (_context.PlayerImage == null)
            {
                return NotFound();
            }
            var playerImage = await _context.PlayerImage.FindAsync(id);
            if (playerImage == null)
            {
                return NotFound();
            }

            _context.PlayerImage.Remove(playerImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerImageExists(int id)
        {
            return (_context.PlayerImage?.Any(e => e.PlayerImageID == id)).GetValueOrDefault();
        }
    }
}
