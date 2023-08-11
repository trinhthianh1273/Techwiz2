using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesAPIController : ControllerBase
    {
        private readonly SoccerContext _context;

        public MatchesAPIController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/MatchesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatch()
        {
          if (_context.Match == null)
          {
              return NotFound();
          }
            return await _context.Match.ToListAsync();
        }

        // GET: api/MatchesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
          if (_context.Match == null)
          {
              return NotFound();
          }
            var match = await _context.Match.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/MatchesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            if (id != match.MatchId)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/MatchesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
          if (_context.Match == null)
          {
              return Problem("Entity set 'SoccerContext.Match'  is null.");
          }
            _context.Match.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatch", new { id = match.MatchId }, match);
        }

        [HttpGet("{competitionName}")]
        public async Task<ActionResult<Match>> SearchByCompetitionName(string competitionName)
        {
            if (_context.Match == null)
            {
                return Problem("Entity set 'SoccerContext.Match'  is null.");
            }
            var matches = _context.Match.Where(item => item.Competition.CompetitionName == competitionName).ToListAsync();
            return Ok(matches);
        }

        // DELETE: api/MatchesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            if (_context.Match == null)
            {
                return NotFound();
            }
            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Match.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(int id)
        {
            return (_context.Match?.Any(e => e.MatchId == id)).GetValueOrDefault();
        }
    }
}
