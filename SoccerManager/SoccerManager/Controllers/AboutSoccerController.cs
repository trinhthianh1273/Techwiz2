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
    public class AboutSoccerController : Controller
    {
        private readonly SoccerContext _context;

        public AboutSoccerController(SoccerContext context)
        {
            _context = context;
        }

        // GET: AboutSoccer
        public async Task<IActionResult> Index()
        {
            return View();
        }
        
        public async Task<IActionResult> SoccerInfoBasic()
        {
            return View();
        }

        public async Task<IActionResult> SoccerVsFootball()
        {
            return View();
        }

        public async Task<IActionResult> SoccerRules()
        {
            return View();
        }

        public async Task<IActionResult> SoccerHistory()
        {
            return View();
        }

        public async Task<IActionResult> SoccerField()
        {
            return View();
        }

        public async Task<IActionResult> SoccerStatics()
        {
            return View();
        }



    }
}
