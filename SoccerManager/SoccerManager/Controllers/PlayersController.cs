using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Interfaces;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly SoccerContext _context;
        private readonly IFileUploadService _fileUploadService;
        private readonly string ImgDir = "PlayerImages";
        private readonly string ImgType = "player";

        public PlayersController(SoccerContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;

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
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Details/5
        // Get Player detail
        public async Task<IActionResult> PlayerInfomation(int? id)
        {
            if (id == null || _context.Player == null)
            {
                return NotFound();
            }
            var playerImage = await _context.PlayerImage
                .Include(p => p.Player)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (playerImage == null)
            {
                ViewBag.PlayerImage = null;
            }
            else
            {
                ViewBag.PlayerImage = playerImage.ImageUrl;
            }

            var player = await _context.Player
                .Include(p => p.CurrentTeamNavigation)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName");
            return View();
        }

        // POST: Players/Create
        // Handle Create Player request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("PlayerId,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player,
            List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                //add product to database
                _context.Add(player);
                await _context.SaveChangesAsync();

                //get last inserted product id
                var lastInsertedId = player.PlayerId;

                //check request files
                if (files != null)
                {
                    //upload image to folder and image nam to database
                    foreach (var item in files)
                    {
                        try
                        {
                            //upload image and get file name
                            string imgName = ImgDir + "/" + await _fileUploadService.UploadFile(item, ImgDir, ImgType);
                            //save filename to database
                            PlayerImage playerImage = new()
                            {
                                PlayerId = lastInsertedId,
                                ImageUrl = imgName
                            };
                            _context.PlayerImage.Add(playerImage);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            //Log ex
                            ViewBag.Message = "File Upload Failed";
                        }
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName", player.CurrentTeam);
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


            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName", player.CurrentTeam);

            ViewBag.PlayerImages = _context.PlayerImage.Where(p => p.PlayerId == id).ToList();
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player)
        {
            if (id != player.PlayerId)
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
                    if (!PlayerExists(player.PlayerId))
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
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName", player.CurrentTeam);
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
                .FirstOrDefaultAsync(m => m.PlayerId == id);
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
            return (_context.Player?.Any(e => e.PlayerId == id)).GetValueOrDefault();
        }
    }
}
