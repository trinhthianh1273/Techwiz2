using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Interfaces;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
	public class TeamsController : Controller
	{
		private readonly SoccerContext _context;

		private readonly IFileUploadService _fileUploadService;

		private readonly string ImgDir = "TeamLogos";
		private readonly string ImgType = "team";

		public TeamsController(SoccerContext context, IFileUploadService fileUploadService)
		{
			_context = context;
			_fileUploadService = fileUploadService;
		}

		// GET: Teams
		// View Admin
		// Get all Teams
		public async Task<IActionResult> Index()
		{
			return _context.Team != null ?
						View(await _context.Team.ToListAsync()) :
						Problem("Entity set 'SoccerContext.Team'  is null.");
		}
		// GET: Teams/ListTeam
		// View User
		// Get all Teams
		public async Task<IActionResult> ListTeam()
		{
			return _context.Team != null ?
						View(await _context.Team.ToListAsync()) :
						Problem("Entity set 'SoccerContext.Team'  is null.");
		}

<<<<<<< HEAD
        // GET: Teams
        // View Admin
        // Get all Teams
        public async Task<IActionResult> Index()
        {
            return _context.Team != null ?
                        View(await _context.Team.ToListAsync()) :
                        Problem("Entity set 'SoccerContext.Team'  is null.");
        }
        // GET: Teams/ListTeam
        // View User
        // Get all Teams
        public async Task<IActionResult> ListTeam()
        {
            return _context.Team != null ?
                        View(await _context.Team.ToListAsync()) :
                        Problem("Entity set 'SoccerContext.Team'  is null.");
        }
=======
>>>>>>> Develop

        // GET: Teams/Details/5
        // View Admin
        // Get Team detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }
			var team = await _context.Team
				.FirstOrDefaultAsync(m => m.TeamId == id);
			if (team == null)
			{
				return NotFound();
			}
			return View(team);
		}

		// GET: Teams/TeamInfo/5
		// View User
		// Get Team detail
		public async Task<IActionResult> TeamInfo(int? id)
		{
			if (id == null || _context.Team == null)
			{
				return NotFound();
			}

<<<<<<< HEAD
            return View(team);
        }
=======
			var team = await _context.Team
				.FirstOrDefaultAsync(m => m.TeamId == id);
>>>>>>> Develop

			ViewBag.Player = _context.Player
				.Where(p => p.CurrentTeam == id)
				.Include(i => i.PlayerImage)
				.ToList();

			ViewBag.Images = _context.PlayerImage.ToList();
			if (team == null)
			{
				return NotFound();
			}

			return View(team);
		}
        

<<<<<<< HEAD
            ViewBag.Images = _context.PlayerImage.ToList();
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }


=======
		
>>>>>>> Develop
        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }


		// POST
		// Handle Team POST Request
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("TeamId,FullName,ShortName,Nickname,FoundedYear,FoundedPosition,Owner,Manager,Website")] Team team, IFormFile file)
		{
			if (ModelState.IsValid)
			{
				try
				{
					//upload image and get file name
					team.LogoUrl = ImgDir + "/" + await _fileUploadService.UploadFile(file, ImgDir, ImgType);
				}
				catch (Exception ex)
				{
					//Log ex
					ViewBag.Message = "File Upload Failed";
				}

				//insert to database
				_context.Add(team);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}


			return View(team);
		}

<<<<<<< HEAD
        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
=======
		// GET: Teams/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Team == null)
			{
				return NotFound();
			}
			var team = await _context.Team.FindAsync(id);
			if (team == null)
			{
				return NotFound();
			}
			return View(team);
		}

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
>>>>>>> Develop
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }
<<<<<<< HEAD
            var team = await _context.Team.FindAsync(id);
=======

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamId == id);
>>>>>>> Develop
            if (team == null)
            {
                return NotFound();
            }
<<<<<<< HEAD
            return View(team);
        }

=======

            return View(team);
        }


>>>>>>> Develop
        // POST: Teams/Edit/5
        // Handle Team PUT Request
        [HttpPost]
        [ValidateAntiForgeryToken]
<<<<<<< HEAD
        public async Task<IActionResult> Edit(
            int id,
            string LogoUrl,
            [Bind("TeamId,FullName,ShortName,Nickname,FoundedYear,FoundedPosition,Owner,Manager,Website")] Team team,
            IFormFile file
            )
=======
        public async Task<IActionResult> Edit(int id, string LogoUrl, [Bind("TeamId,FullName,ShortName,Nickname,FoundedYear,FoundedPosition,Owner,Manager,Website")] Team team, IFormFile file)
>>>>>>> Develop
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }
            try
            {
                //get current logo url
                var currentImg = LogoUrl;

				//if logo is changed, delete old one from folder
				if (file != null)
				{
					//delete current image from folder
					if (currentImg != null)
						await _fileUploadService.DeleteFile(currentImg);

					//set new LogoUrl
					team.LogoUrl = ImgDir + "/" + await _fileUploadService.UploadFile(file, ImgDir, ImgType);
				}
				else
				{
					team.LogoUrl = currentImg;  //logo still remains unchanged
				}

				//update database
				try
				{
					_context.Update(team);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!TeamExists(team.TeamId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}

			catch (Exception ex)
			{
				return View(team);
			}
			return RedirectToAction(nameof(Index));

		}

		// POST: Teams/Delete/5
		// Handle DELETE Team Request
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Team == null)
			{
				return Problem("Entity set 'SoccerContext.Team'  is null.");
			}

			//get team object
			var team = await _context.Team.FindAsync(id);
			if (team != null)
			{
				//delete team logo in folder
				await _fileUploadService.DeleteFile(team.LogoUrl);

				//delete team in database
				_context.Team.Remove(team);
			}
			//commit changes
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}



		private bool TeamExists(int id)
		{
			return (_context.Team?.Any(e => e.TeamId == id)).GetValueOrDefault();
		}
	}

<<<<<<< HEAD
        private bool TeamExists(int id)
        {
            return (_context.Team?.Any(e => e.TeamId == id)).GetValueOrDefault();
        }
    }
=======
>>>>>>> Develop
}
