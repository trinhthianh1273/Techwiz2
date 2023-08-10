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
                .FirstOrDefaultAsync(m => m.TeamId == id);
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



        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamId,FullName,ShortName,Nickname,FoundedYear,FoundedPosition,Owner,Manager,Website")] Team team, List<IFormFile> file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var f = file.ElementAt(0);
                    string ImageName = await _fileUploadService.UploadFile(f, "team");
                    team.TeamImage
                }
                catch (Exception ex)
                {
                    //Log ex
                    ViewBag.Message = "File Upload Failed";
                }

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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamId,FullName,ShortName,Nickname,FoundedYear,FoundedPosition,Owner,Manager,Website")] Team team)
        {
            if (id != team.TeamId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            return View(team);
        }

        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Team == null)
            {
                return Problem("Entity set 'SoccerContext.Team'  is null.");
            }
            var team = await _context.Team.FindAsync(id);
            if (team != null)
            {
                _context.Team.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return (_context.Team?.Any(e => e.TeamId == id)).GetValueOrDefault();
        }


        //upload file to 
        private async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            try
            {
                if (file.Length > 0)
                {
                    path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File Copy Failed", ex);
            }
        }

    }
}
