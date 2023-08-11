using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly SoccerContext _context;

        public PlayersController(SoccerContext context)
        {
            _context = context;
        }

        // GET: Players
        // Get list of Playes
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.Player.Include(p => p.CurrentTeamNavigation);
            return View(await soccerContext.ToListAsync());
        }

        // GET: Players/Details/5
        // Get Player detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.CurrentTeamNavigation)
                .FirstOrDefaultAsync(m => m.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamID", "FullName");
            return View();
        }

        // POST: Players/Create
        // Handle Create Player request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerID,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                //add player information
                _context.Add(player);
                await _context.SaveChangesAsync();

                //upload player image


                return RedirectToAction(nameof(Index));
            }

            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamID", "FullName", player.CurrentTeam);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamID", "FullName", player.CurrentTeam);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerID,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player)
        {
            if (id != player.PlayerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlayerID))
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
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamID", "FullName", player.CurrentTeam);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }

            var player = await _context.Player
                .Include(p => p.CurrentTeamNavigation)
                .FirstOrDefaultAsync(m => m.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'SoccerContext.Player'  is null.");
            }
            var player = await _context.Player.FindAsync(id);
            if (player != null)
            {
                _context.Player.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(int id)
        {
            return (_context.Player?.Any(e => e.PlayerID == id)).GetValueOrDefault();
        }
    }
}
