using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Interfaces;
using SoccerManager.Models;
using X.PagedList;

namespace SoccerManager.Controllers
{

    public class PlayersController : Controller
    {
        private readonly SoccerContext _context;
<<<<<<< HEAD
=======

>>>>>>> Develop
        private readonly IWebHostEnvironment _env;
        private readonly IFileUploadService _fileUploadService;
        private readonly string ImgDir = "PlayerImages";
        private readonly string ImgType = "player";

        public PlayersController(SoccerContext context, IWebHostEnvironment env, IFileUploadService fileUploadService)
        {
            _context = context;
            _env = env;
            _fileUploadService = fileUploadService;
        }
        

        // GET: Players
        // Admin view
        // Get list of Playes
        public async Task<IActionResult> PlayerInfomation(int? id, int? page)
        {
            
            var soccerContext = _context.Player
                .Where(p => p.PlayerId == id)
                .Include(i => i.PlayerImage)
                .Single();
            ViewBag.Team = _context.Team.Find(soccerContext.CurrentTeam).FullName;
            ViewBag.PlayerImage = soccerContext.PlayerImage.Count == 0 ? null : soccerContext.PlayerImage.ElementAt(0).ImageUrl;

            return View(soccerContext);
        }

<<<<<<< HEAD
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
                .Include(i => i.PlayerImage)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (player == null)
            {
                return NotFound();
            }
=======
		// GET: Players
		// Admin view
		// Get list of Playes
		public async Task<IActionResult> Index(int? page)
		{
            int pageSize = 10; // Số sản phẩm trên mỗi trang
            int pageNumber = (page ?? 1); // Trang hiện tại, mặc định là trang 1

            var soccerContext = _context.Player.Include(p => p.CurrentTeamNavigation);

            IPagedList<Player> pagedPlayers = soccerContext.ToPagedList(pageNumber, pageSize);

            ViewBag.PagedPlayers = pagedPlayers;
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
				.Include(i => i.PlayerImage)
				.FirstOrDefaultAsync(m => m.PlayerId == id);
			if (player == null)
			{
				return NotFound();
			}
>>>>>>> Develop

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
            [Bind("PlayerId,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player
            , List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();

                //get last inserted player id
                var lastInsertedId = player.PlayerId;

                //check request files
                if (files != null)
                {
                    //upload image
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

            var player = _context.Player.Where(p => p.PlayerId == id).Include(i => i.PlayerImage).Single();
            if (player == null)
            {
                return NotFound();
            }
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName", player.CurrentTeam);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("PlayerId,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player,
            string[] deleteImage,
            List<IFormFile> files)
        {
            if (id != player.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //delete selected image 
                    if (deleteImage.Length > 0)
                    {
                        foreach (var img in deleteImage)
                        {
                            await _fileUploadService.DeleteFile(img);   //delete from folder
                            _context.Remove(_context.PlayerImage.Where(p => p.ImageUrl == img).FirstOrDefault());  //delete from database
                        }
                    }

                    //add new image
                    if (files != null)
                    {
                        foreach (var item in files)
                        {
                            //upload image and get file name
                            string imgName = ImgDir + "/" + await _fileUploadService.UploadFile(item, ImgDir, ImgType);
                            //save filename to database
                            PlayerImage playerImage = new()
                            {
                                PlayerId = player.PlayerId,
                                ImageUrl = imgName
                            };
                            _context.PlayerImage.Add(playerImage);
                            await _context.SaveChangesAsync();
                        }
                    }

                    //update information
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


        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Player == null)
            {
                return Problem("Entity set 'SoccerContext.Player'  is null.");
            }
            //get current player
            var player = await _context.Player.FindAsync(id);

            if (player != null)
            {
                //get images list
                var images = await _context.PlayerImage.Where(pi => pi.PlayerId == player.PlayerId).ToListAsync();

                //delete images on local folder and database
                foreach (var img in images)
                {
                    await _fileUploadService.DeleteFile(img.ImageUrl);
                    _context.PlayerImage.Remove(img);
                }

                //delete product information
                _context.Player.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

<<<<<<< HEAD
        private bool PlayerExists(int id)
        {
            return (_context.Player?.Any(e => e.PlayerId == id)).GetValueOrDefault();
        }
=======

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
		private bool PlayerExists(int id)
		{
			return (_context.Player?.Any(e => e.PlayerId == id)).GetValueOrDefault();
		}
>>>>>>> Develop
    }
}
