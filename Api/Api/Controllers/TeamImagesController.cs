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
    public class TeamImagesController : ControllerBase
    {
        private readonly SoccerContext _context;

        public TeamImagesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/TeamImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamImage>>> GetTeamImage()
        {
          if (_context.TeamImage == null)
          {
              return NotFound();
          }
            return await _context.TeamImage.ToListAsync();
        }

        // GET: api/TeamImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamImage>> GetTeamImage(int id)
        {
          if (_context.TeamImage == null)
          {
              return NotFound();
          }
            var teamImage = await _context.TeamImage.FindAsync(id);

            if (teamImage == null)
            {
                return NotFound();
            }

            return teamImage;
        }

        // PUT: api/TeamImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamImage(int id, TeamImage teamImage)
        {
            if (id != teamImage.TeamImageID)
            {
                return BadRequest();
            }

            _context.Entry(teamImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamImageExists(id))
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

        // POST: api/TeamImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeamImage>> PostTeamImage(TeamImage teamImage)
        {
          if (_context.TeamImage == null)
          {
              return Problem("Entity set 'SoccerContext.TeamImage'  is null.");
          }
            _context.TeamImage.Add(teamImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamImage", new { id = teamImage.TeamImageID }, teamImage);
        }

        // DELETE: api/TeamImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamImage(int id)
        {
            if (_context.TeamImage == null)
            {
                return NotFound();
            }
            var teamImage = await _context.TeamImage.FindAsync(id);
            if (teamImage == null)
            {
                return NotFound();
            }

            _context.TeamImage.Remove(teamImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamImageExists(int id)
        {
            return (_context.TeamImage?.Any(e => e.TeamImageID == id)).GetValueOrDefault();
        }
    }
}
