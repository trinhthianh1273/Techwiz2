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
    public class TeamPlayerHistoriesController : ControllerBase
    {
        private readonly SoccerContext _context;

        public TeamPlayerHistoriesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/TeamPlayerHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamPlayerHistory>>> GetTeamPlayerHistory()
        {
          if (_context.TeamPlayerHistory == null)
          {
              return NotFound();
          }
            return await _context.TeamPlayerHistory.ToListAsync();
        }

        // GET: api/TeamPlayerHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamPlayerHistory>> GetTeamPlayerHistory(int id)
        {
          if (_context.TeamPlayerHistory == null)
          {
              return NotFound();
          }
            var teamPlayerHistory = await _context.TeamPlayerHistory.FindAsync(id);

            if (teamPlayerHistory == null)
            {
                return NotFound();
            }

            return teamPlayerHistory;
        }

        // PUT: api/TeamPlayerHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamPlayerHistory(int id, TeamPlayerHistory teamPlayerHistory)
        {
            if (id != teamPlayerHistory.ID)
            {
                return BadRequest();
            }

            _context.Entry(teamPlayerHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamPlayerHistoryExists(id))
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

        // POST: api/TeamPlayerHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeamPlayerHistory>> PostTeamPlayerHistory(TeamPlayerHistory teamPlayerHistory)
        {
          if (_context.TeamPlayerHistory == null)
          {
              return Problem("Entity set 'SoccerContext.TeamPlayerHistory'  is null.");
          }
            _context.TeamPlayerHistory.Add(teamPlayerHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamPlayerHistory", new { id = teamPlayerHistory.ID }, teamPlayerHistory);
        }

        // DELETE: api/TeamPlayerHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamPlayerHistory(int id)
        {
            if (_context.TeamPlayerHistory == null)
            {
                return NotFound();
            }
            var teamPlayerHistory = await _context.TeamPlayerHistory.FindAsync(id);
            if (teamPlayerHistory == null)
            {
                return NotFound();
            }

            _context.TeamPlayerHistory.Remove(teamPlayerHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamPlayerHistoryExists(int id)
        {
            return (_context.TeamPlayerHistory?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
