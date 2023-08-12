using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Interfaces;
using SoccerManager.Models;


namespace SoccerManager.Controllers
{
    public class NewsController : Controller
    {
        private readonly SoccerContext _context;

        private readonly IFileUploadService _fileUploadService;

        private readonly string ImgDir = "NewsImages";
        private readonly string ImgType = "news";

        public NewsController(SoccerContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }


        // GET: News
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.News.Include(n => n.Employee);
            return View(await soccerContext.ToListAsync());
        }

        // GET: List News
        // User view
        public async Task<IActionResult> Articles()
        {
            var soccerContext = _context.News.Include(n => n.Employee);
            return View(await soccerContext.ToListAsync());
        }

        // GET: News/ArticleDetail/5
        public async Task<IActionResult> ArticleDetail(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Employee)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }


        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Employee)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FullName");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsId,Title,Summary,Contents,ImageUrl,EmployeeId")] News news, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //upload image and get file name
                    news.ImageUrl = ImgDir + "/" + await _fileUploadService.UploadFile(file, ImgDir, ImgType);
                }
                catch (Exception ex)
                {
                    //Log ex
                    ViewBag.Message = "File Upload Failed";
                }

                //insert to database
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FullName", news.EmployeeId);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Email", news.EmployeeId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            string LogoUrl,
            [Bind("NewsId,Title,Summary,Contents,ImageUrl,EmployeeId")] News news,
            IFormFile file)
        {
            if (id != news.NewsId)
            {
                return NotFound();
            }

            //get current logo url
            var currentImg = LogoUrl;

            //if logo is changed, delete old one from folder
            if (file != null)
            {
                //delete current image from folder
                if (currentImg != null)
                    await _fileUploadService.DeleteFile(currentImg);

                //set new LogoUrl
                news.ImageUrl = ImgDir + "/" + await _fileUploadService.UploadFile(file, ImgDir, ImgType);
            }
            else
            {
                news.ImageUrl = currentImg;  //logo still remains unchanged
            }

            //update database
            try
            {
                _context.Update(news);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsExists(news.NewsId))
                {
                    return NotFound();
                }
                else
                {
                    ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FullName", news.EmployeeId);
                    return View(news);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.News == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Employee)
                .FirstOrDefaultAsync(m => m.NewsId == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.News == null)
            {
                return Problem("Entity set 'SoccerContext.News'  is null.");
            }
            var news = await _context.News.FindAsync(id);
            if (news != null)
            {
                _context.News.Remove(news);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return (_context.News?.Any(e => e.NewsId == id)).GetValueOrDefault();
        }
    }
}
