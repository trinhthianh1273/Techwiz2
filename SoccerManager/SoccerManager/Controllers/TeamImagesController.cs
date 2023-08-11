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
    public class TeamImagesController : Controller
    {
        private readonly SoccerContext _context;

        public TeamImagesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: TeamImages
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.TeamImage.Include(t => t.Team);
            return View(await soccerContext.ToListAsync());
        }

        // GET: TeamImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TeamImage == null)
            {
                return NotFound();
            }

            var teamImage = await _context.TeamImage
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamImageID == id);
            if (teamImage == null)
            {
                return NotFound();
            }

            return View(teamImage);
        }

        // GET: TeamImages/Create
        public IActionResult Create()
        {
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition");
            return View();
        }

        // POST: TeamImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamImageID,TeamID,ImageURL")] TeamImage teamImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", teamImage.TeamID);
            return View(teamImage);
        }

        // GET: TeamImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TeamImage == null)
            {
                return NotFound();
            }

            var teamImage = await _context.TeamImage.FindAsync(id);
            if (teamImage == null)
            {
                return NotFound();
            }
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", teamImage.TeamID);
            return View(teamImage);
        }

        // POST: TeamImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamImageID,TeamID,ImageURL")] TeamImage teamImage)
        {
            if (id != teamImage.TeamImageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamImageExists(teamImage.TeamImageID))
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
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FoundedPosition", teamImage.TeamID);
            return View(teamImage);
        }

        // GET: TeamImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TeamImage == null)
            {
                return NotFound();
            }

            var teamImage = await _context.TeamImage
                .Include(t => t.Team)
                .FirstOrDefaultAsync(m => m.TeamImageID == id);
            if (teamImage == null)
            {
                return NotFound();
            }

            return View(teamImage);
        }

        // POST: TeamImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TeamImage == null)
            {
                return Problem("Entity set 'SoccerContext.TeamImage'  is null.");
            }
            var teamImage = await _context.TeamImage.FindAsync(id);
            if (teamImage != null)
            {
                _context.TeamImage.Remove(teamImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamImageExists(int id)
        {
          return (_context.TeamImage?.Any(e => e.TeamImageID == id)).GetValueOrDefault();
        }
    }
}
