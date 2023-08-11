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
    public class TeamPlayerHistoriesController : Controller
    {
        private readonly SoccerContext _context;

        public TeamPlayerHistoriesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: TeamPlayerHistories
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.TeamPlayerHistory.Include(t => t.Player).Include(t => t.Team);
            return View(await soccerContext.ToListAsync());
        }

        // GET: TeamPlayerHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeamPlayerHistory == null)
            {
                return NotFound();
            }

            var teamPlayerHistory = await _context.TeamPlayerHistory
                .Include(t => t.Player)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teamPlayerHistory == null)
            {
                return NotFound();
            }

            return View(teamPlayerHistory);
        }

        // GET: TeamPlayerHistories/Create
        public IActionResult Create()
        {
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName");
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition");
            return View();
        }

        // POST: TeamPlayerHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeamID,PlayerID,JoinDate,LeaveDate")] TeamPlayerHistory teamPlayerHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamPlayerHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", teamPlayerHistory.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", teamPlayerHistory.TeamID);
            return View(teamPlayerHistory);
        }

        // GET: TeamPlayerHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeamPlayerHistory == null)
            {
                return NotFound();
            }

            var teamPlayerHistory = await _context.TeamPlayerHistory.FindAsync(id);
            if (teamPlayerHistory == null)
            {
                return NotFound();
            }
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", teamPlayerHistory.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", teamPlayerHistory.TeamID);
            return View(teamPlayerHistory);
        }

        // POST: TeamPlayerHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeamID,PlayerID,JoinDate,LeaveDate")] TeamPlayerHistory teamPlayerHistory)
        {
            if (id != teamPlayerHistory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamPlayerHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamPlayerHistoryExists(teamPlayerHistory.ID))
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
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", teamPlayerHistory.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", teamPlayerHistory.TeamID);
            return View(teamPlayerHistory);
        }

        // GET: TeamPlayerHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeamPlayerHistory == null)
            {
                return NotFound();
            }

            var teamPlayerHistory = await _context.TeamPlayerHistory
                .Include(t => t.Player)
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teamPlayerHistory == null)
            {
                return NotFound();
            }

            return View(teamPlayerHistory);
        }

        // POST: TeamPlayerHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeamPlayerHistory == null)
            {
                return Problem("Entity set 'SoccerContext.TeamPlayerHistory'  is null.");
            }
            var teamPlayerHistory = await _context.TeamPlayerHistory.FindAsync(id);
            if (teamPlayerHistory != null)
            {
                _context.TeamPlayerHistory.Remove(teamPlayerHistory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamPlayerHistoryExists(int id)
        {
          return (_context.TeamPlayerHistory?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
