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
            var matches = _context.Match.Include(m => m.Competition).Include(m => m.GuestTeam).Include(m => m.HomeTeam).ToList();
            //var players = 
            ViewBag.Matches = matches;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}