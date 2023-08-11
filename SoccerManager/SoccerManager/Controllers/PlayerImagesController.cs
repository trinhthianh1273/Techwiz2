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
    public class PlayerImagesController : Controller
    {
        private readonly SoccerContext _context;

        public PlayerImagesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: PlayerImages
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.PlayerImage.Include(p => p.Player);
            return View(await soccerContext.ToListAsync());
        }

        // GET: PlayerImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlayerImage == null)
            {
                return NotFound();
            }

            var playerImage = await _context.PlayerImage
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PlayerImageID == id);
            if (playerImage == null)
            {
                return NotFound();
            }

            return View(playerImage);
        }

        // GET: PlayerImages/Create
        public IActionResult Create()
        {
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName");
            return View();
        }

        // POST: PlayerImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerImageID,PlayerID,ImageURL")] PlayerImage playerImage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerImage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", playerImage.PlayerID);
            return View(playerImage);
        }

        // GET: PlayerImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlayerImage == null)
            {
                return NotFound();
            }

            var playerImage = await _context.PlayerImage.FindAsync(id);
            if (playerImage == null)
            {
                return NotFound();
            }
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", playerImage.PlayerID);
            return View(playerImage);
        }

        // POST: PlayerImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerImageID,PlayerID,ImageURL")] PlayerImage playerImage)
        {
            if (id != playerImage.PlayerImageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerImage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerImageExists(playerImage.PlayerImageID))
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
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", playerImage.PlayerID);
            return View(playerImage);
        }

        // GET: PlayerImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlayerImage == null)
            {
                return NotFound();
            }

            var playerImage = await _context.PlayerImage
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PlayerImageID == id);
            if (playerImage == null)
            {
                return NotFound();
            }

            return View(playerImage);
        }

        // POST: PlayerImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlayerImage == null)
            {
                return Problem("Entity set 'SoccerContext.PlayerImage'  is null.");
            }
            var playerImage = await _context.PlayerImage.FindAsync(id);
            if (playerImage != null)
            {
                _context.PlayerImage.Remove(playerImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerImageExists(int id)
        {
          return (_context.PlayerImage?.Any(e => e.PlayerImageID == id)).GetValueOrDefault();
        }
    }
}
