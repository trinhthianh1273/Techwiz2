using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;
using System.Diagnostics;

namespace SoccerManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SoccerContext _context;

        public HomeController(ILogger<HomeController> logger, SoccerContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            if(HttpContext.Session.GetString("CustomerId") != null)
            {
                int customerId = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
                var carts = _context.Cart
                                .Where(c => c.CustomerId == customerId)
                                .Include(c => c.Product)
                                .ToList();
                ViewBag.Carts = carts;
				ViewData["Carts"] = carts;
			}
			else
            {
                ViewBag.Carts = null;
				ViewData["Carts"] = null;

			}

			var matches = _context.Match
                .Include(m => m.Competition)
                .Include(m => m.GuestTeam)
                .Include(m => m.HomeTeam)
                .ToList();

            var players = _context.Player
                .OrderByDescending(p => p.Scores)
                .Include(p => p.CurrentTeamNavigation)
                .Include(p => p.TeamPlayerHistory)
                .Include(p => p.PlayerImage)
                .Take(10)
                .ToList();

            var news = _context.News.Include(n => n.Employee);
            ViewBag.News = news.OrderBy(p => p.NewsId).ToList() ;

            ViewBag.Matches = matches;
            ViewBag.Top10Players = players ;
			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
