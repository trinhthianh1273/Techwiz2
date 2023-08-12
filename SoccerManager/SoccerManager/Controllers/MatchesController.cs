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
		// admin view
		public async Task<IActionResult> Index()
		{
			var soccerContext = _context.Products
				.Include(p => p.Category)
				.Include(p => p.Player)
				.Include(p => p.Team)
				.Include(p => p.ProductImage);

			var matches = _context.Match
				.Include(m => m.Competition)
				.Include(m => m.GuestTeam)
				.Include(m => m.HomeTeam);
			ViewBag.Products = soccerContext.ToList();

			return View(await matches.ToListAsync());
		}

		// GET: Matches
		// user view
		public async Task<IActionResult> Matches()
		{

			var matches = _context.Match
				.Include(m => m.Competition)
				.Include(m => m.GuestTeam)
				.Include(m => m.HomeTeam);

			ViewBag.Lattest = matches.OrderBy(m => m.MatchId).Last();

			return View(await matches.ToListAsync());
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
				.FirstOrDefaultAsync(m => m.MatchId == id);
			if (match == null)
			{
				return NotFound();
			}

			return View(match);
		}

		// GET: Matches/Create
		public IActionResult Create()
		{
			ViewData["CompetitionId"] = new SelectList(
				_context.Competition,
				"CompetitionId",
				"CompetitionName"
			);
			ViewData["GuestTeamId"] = new SelectList(_context.Team, "TeamId", "ShortName");
			ViewData["HomeTeamId"] = new SelectList(_context.Team, "TeamId", "ShortName");
			return View();
		}

		// POST: Matches/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind(
				"MatchId,MatchName,HomeTeamId,GuestTeamId,StartTime,EndTime,Stadium,HomeTeamScore,GuestTeamScore,CompetitionId,Description"
			)]
				Match match
		)
		{
			if (ModelState.IsValid)
			{
				_context.Add(match);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CompetitionId"] = new SelectList(
				_context.Competition,
				"CompetitionId",
				"CompetitionName",
				match.CompetitionId
			);
			ViewData["GuestTeamId"] = new SelectList(
				_context.Team,
				"TeamId",
				"ShortName",
				match.GuestTeamId
			);
			ViewData["HomeTeamId"] = new SelectList(
				_context.Team,
				"TeamId",
				"ShortName",
				match.HomeTeamId
			);
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
			ViewData["CompetitionId"] = new SelectList(
				_context.Competition,
				"CompetitionId",
				"CompetitionName",
				match.CompetitionId
			);
			ViewData["GuestTeamId"] = new SelectList(
				_context.Team,
				"TeamId",
				"FoundedPosition",
				match.GuestTeamId
			);
			ViewData["HomeTeamId"] = new SelectList(
				_context.Team,
				"TeamId",
				"FoundedPosition",
				match.HomeTeamId
			);
			return View(match);
		}

		// POST: Matches/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(
			int id,
			[Bind(
				"MatchId,MatchName,HomeTeamId,GuestTeamId,StartTime,EndTime,Stadium,HomeTeamScore,GuestTeamScore,CompetitionId,Description"
			)]
				Match match
		)
		{
			if (id != match.MatchId)
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
					if (!MatchExists(match.MatchId))
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
			ViewData["CompetitionId"] = new SelectList(
				_context.Competition,
				"CompetitionId",
				"CompetitionName",
				match.CompetitionId
			);
			ViewData["GuestTeamId"] = new SelectList(
				_context.Team,
				"TeamId",
				"FoundedPosition",
				match.GuestTeamId
			);
			ViewData["HomeTeamId"] = new SelectList(
				_context.Team,
				"TeamId",
				"FoundedPosition",
				match.HomeTeamId
			);
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
				.FirstOrDefaultAsync(m => m.MatchId == id);
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
			return (_context.Match?.Any(e => e.MatchId == id)).GetValueOrDefault();
		}
	}
}
