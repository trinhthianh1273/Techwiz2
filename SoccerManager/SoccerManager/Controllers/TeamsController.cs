﻿using Microsoft.AspNetCore.Mvc;
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
        // Get all Teams
        public async Task<IActionResult> Index()
        {
            return _context.Team != null ?
                        View(await _context.Team.ToListAsync()) :
                        Problem("Entity set 'SoccerContext.Team'  is null.");
        }

        // GET: Teams/Details/5
        // Get Team detail
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }


        // GET: Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST
        // Handle Team POST Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamID,FullName,ShortName,Nickname,FoundedYear,FoundedPosition,Owner,Manager,Website")] Team team, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //upload image and get file name
                    team.LogoURL = ImgDir + "/" + await _fileUploadService.UploadFile(file, ImgDir, ImgType);
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

        // POST: Teams/Edit/5
        // Handle Team PUT Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string LogoURL, [Bind("TeamID,FullName,ShortName,Nickname,FoundedYear,FoundedPosition,Owner,Manager,Website")] Team team, IFormFile file)
        {
            if (id != team.TeamID)
            {
                return NotFound();
            }
            try
            {
                //get current logo url
                var currentImg = LogoURL;

                //if logo is changed, delete old one from folder
                if (file != null)
                {
                    //delete current image from folder
                    if (currentImg != null)
                        await _fileUploadService.DeleteFile(currentImg);

                    //set new LogoUrl
                    team.LogoURL = ImgDir + "/" + await _fileUploadService.UploadFile(file, ImgDir, ImgType);
                }
                else
                {
                    team.LogoURL = currentImg;  //logo still remains unchanged
                }

                //update database
                try
                {
                    _context.Update(team);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(team.TeamID))
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

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Team == null)
            {
                return NotFound();
            }

            var team = await _context.Team
                .FirstOrDefaultAsync(m => m.TeamID == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
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
                await _fileUploadService.DeleteFile(team.LogoURL);

                //delete team in database
                _context.Team.Remove(team);
            }
            //commit changes
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return (_context.Team?.Any(e => e.TeamID == id)).GetValueOrDefault();
        }
    }
}
