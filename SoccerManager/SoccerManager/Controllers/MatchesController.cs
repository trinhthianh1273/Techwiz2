using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    public class MatchesController : Controller
    {
        private readonly SoccerContext _context;

        public MatchesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.Match.Include(m => m.Competition).Include(m => m.GuestTeam).Include(m => m.HomeTeam);
            return View(await soccerContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Match == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.Competition)
                .Include(m => m.GuestTeam)
                .Include(m => m.HomeTeam)
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        public IActionResult Create()
        {
            ViewData["CompetitionID"] = new SelectList(_context.Competition, "CompetitionID", "CompetitionName");
            ViewData["GuestTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition");
            ViewData["HomeTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition");
            return View();
        }

        // POST: Matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MatchID,MatchName,HomeTeamID,GuestTeamID,StartTime,EndTime,Stadium,HomeTeamScore,GuestTeamScore,CompetitionID,Description")] Match match)
        {
            if (ModelState.IsValid)
            {
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompetitionID"] = new SelectList(_context.Competition, "CompetitionID", "CompetitionName", match.CompetitionID);
            ViewData["GuestTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", match.GuestTeamID);
            ViewData["HomeTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", match.HomeTeamID);
            return View(match);
        }

        // GET: Matches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Match == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["CompetitionID"] = new SelectList(_context.Competition, "CompetitionID", "CompetitionName", match.CompetitionID);
            ViewData["GuestTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", match.GuestTeamID);
            ViewData["HomeTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", match.HomeTeamID);
            return View(match);
        }

        // POST: Matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MatchID,MatchName,HomeTeamID,GuestTeamID,StartTime,EndTime,Stadium,HomeTeamScore,GuestTeamScore,CompetitionID,Description")] Match match)
        {
            if (id != match.MatchID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.MatchID))
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
            ViewData["CompetitionID"] = new SelectList(_context.Competition, "CompetitionID", "CompetitionName", match.CompetitionID);
            ViewData["GuestTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", match.GuestTeamID);
            ViewData["HomeTeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", match.HomeTeamID);
            return View(match);
        }

        // GET: Matches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Match == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.Competition)
                .Include(m => m.GuestTeam)
                .Include(m => m.HomeTeam)
                .FirstOrDefaultAsync(m => m.MatchID == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Match == null)
            {
                return Problem("Entity set 'SoccerContext.Match'  is null.");
            }
            var match = await _context.Match.FindAsync(id);
            if (match != null)
            {
                _context.Match.Remove(match);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
          return (_context.Match?.Any(e => e.MatchID == id)).GetValueOrDefault();
        }
    }
}
