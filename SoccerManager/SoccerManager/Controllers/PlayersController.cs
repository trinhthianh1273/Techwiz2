using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Interfaces;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
<<<<<<< Updated upstream
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
=======
	public class PlayersController : Controller
	{
		private readonly SoccerContext _context;
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
>>>>>>> Stashed changes

		// GET: Players
		// View admin
		// Get list of Playes
		public async Task<IActionResult> Index()
		{
			var soccerContext = _context.Player.Include(p => p.CurrentTeamNavigation);
			return View(await soccerContext.ToListAsync());
		}

		// GET: Players
		// View user
		// Get userdetail
		public async Task<IActionResult> PlayerInfomation(int? id)
		{
			var soccerContext = _context.Player.Where(z => z.PlayerId == id).Include(p => p.PlayerImage).Single();
			return View(soccerContext);
		}

<<<<<<< Updated upstream
            var player = await _context.Player
                .Include(p => p.CurrentTeamNavigation)
                .FirstOrDefaultAsync(m => m.PlayerID == id);
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
=======
		// GET: Players/Details/5
		// View admin
		// Get Player detail
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Player == null)
			{
				return NotFound();
			}
>>>>>>> Stashed changes

			var player = await _context.Player
				.Include(p => p.CurrentTeamNavigation)
				.FirstOrDefaultAsync(m => m.PlayerId == id);
			if (player == null)
			{
				return NotFound();
			}

<<<<<<< Updated upstream
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
<<<<<<< HEAD
        public async Task<IActionResult> Create([Bind("PlayerID,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player, IFormFile file)
=======
        public async Task<IActionResult> Create(
            [Bind("PlayerId,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player,
            List<IFormFile> files)
>>>>>>> 8e2198406ca71f7daec9266d036d70ebd9a4995d
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
=======
			return View(player);
		}

		// GET: Players/Create
		// View admin
		public IActionResult Create()
		{
			ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName");
			return View();
		}

		// POST: Players/Create
		// Handle Create Player request
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("PlayerId,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player, List<IFormFile> files)
		{
			//add player to database
			_context.Add(player);
			await _context.SaveChangesAsync();

			//get last inserted player id
			var lastInsertedId = player.PlayerId;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamID", "FullName", player.CurrentTeam);
            return View(player);
        }
=======
				return RedirectToAction(nameof(Index));
			}
>>>>>>> Stashed changes

			ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName", player.CurrentTeam);
			return View(player);
		}

<<<<<<< Updated upstream
            var player = await _context.Player.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
<<<<<<< HEAD
            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamID", "FullName", player.CurrentTeam);
=======


            ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName", player.CurrentTeam);

            ViewBag.PlayerImages = _context.PlayerImage.Where(p => p.PlayerId == id).ToList();
>>>>>>> 8e2198406ca71f7daec9266d036d70ebd9a4995d
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
=======
		// GET: Players/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Player == null)
			{
				return NotFound();
			}

			var player = _context.Player.Where(p => p.PlayerId == id).Include(x => x.PlayerImage).Single();
			if (player == null)
			{
				return NotFound();
			}
			ViewData["CurrentTeam"] = new SelectList(_context.Team, "TeamId", "FullName", player.CurrentTeam);
			return View(player);
		}

		// POST: Players/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(
				int id,
				string[] deleteImage,
				[Bind("PlayerId,FullName,Dob,Pob,Height,Position,CurrentTeam,Number")] Player player,
				List<IFormFile> files
				)
		{
			if (id != player.PlayerId)
			{
				return NotFound();
			}
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
            var player = await _context.Player
                .Include(p => p.CurrentTeamNavigation)
                .FirstOrDefaultAsync(m => m.PlayerID == id);
            if (player == null)
            {
                return NotFound();
            }
=======
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
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
        private bool PlayerExists(int id)
        {
            return (_context.Player?.Any(e => e.PlayerID == id)).GetValueOrDefault();
        }
    }
=======
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
>>>>>>> Stashed changes
}
