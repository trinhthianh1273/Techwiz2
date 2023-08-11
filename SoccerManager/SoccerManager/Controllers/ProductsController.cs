using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Interfaces;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SoccerContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IFileUploadService _fileUploadService;
        private readonly string ImgDir = "ProductImage";
        private readonly string ImgType = "product";

        public ProductsController(SoccerContext context, IWebHostEnvironment env, IFileUploadService fileUploadService)
        {
            _context = context;
            _env = env;
            _fileUploadService = fileUploadService;
        }

        // GET: Products
        // Get list of Products with player and team information
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage);
            return View(await soccerContext.ToListAsync());
        }

        // GET: Shopping
        // Get list of Products with player and team information
        public async Task<IActionResult> Shopping(int? CategoryID)
        {
            var soccerContext = new List<Products>();
            if (CategoryID == null)
            {
                soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage).ToList();
            }
            else
            {

<<<<<<< Updated upstream
				if (CategoryID!=null)
				{
					soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage).Where(p => p.CategoryID == CategoryID).ToList();
				}
			}    
			
            
=======

                if (CategoryID != null)
                {
                    soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage).Where(p => p.CategoryId == CategoryID).ToList();
                }
            }


>>>>>>> Stashed changes
            var categories = _context.Category.Include(p => p.Products).ToList();
            ViewBag.Categories = categories;
            return View(soccerContext);
        }
        [HttpPost]
        public async Task<IActionResult> Shopping(string SearchString)
        {
            var soccerContext = new List<Products>();
            if (SearchString is null)
            {
                soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage).ToList();
            }
            else
            {
                soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage).Where(p => p.ProductName.Contains(SearchString)).ToList();
            }


            var categories = _context.Category.Include(p => p.Products).ToList();
            ViewBag.Categories = categories;
            return View(soccerContext);
        }

        // GET: Products/Details/5
        // Get detail for 1 product
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Player)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName");
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName");
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FullName");
            return View();
        }

        // POST: Products/Create
        // Handle create Product request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,Description,CategoryID,Price,InStock,OnOrder,Discontinued,TeamID,PlayerID")] Products products, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                //add product to database
                _context.Add(products);
                await _context.SaveChangesAsync();

                //get last inserted product id
                var lastInsertedId = products.ProductID;

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
                            ProductImage productImage = new()
                            {
                                ProductID = lastInsertedId,
                                ImageURL = imgName
                            };
                            _context.ProductImage.Add(productImage);
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", products.CategoryID);
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", products.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FullName", products.TeamID);

            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            //get detail
            var products = _context.Products.Where(p => p.ProductID == id).Include(pi => pi.ProductImage).Single();

            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryDescription", products.CategoryID);
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", products.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FullName", products.TeamID);
            return View(products);
        }

        // POST: Products/Edit/5
        // Handle PUT Product Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
                    int id,
                    [Bind("ProductID,ProductName,Description,CategoryID,Price,InStock,OnOrder,Discontinued,TeamID,PlayerID")] Products products,
                    string[] deleteImage,
                    List<IFormFile> files)
        {
            if (id != products.ProductID)
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
                            _context.Remove(_context.ProductImage.Where(p => p.ImageURL == img).FirstOrDefault());  //delete from database
                        }
                    }

                    //add new image
                    if (files != null)
                    {
                        foreach (var item in files)
                        {
                            //upload image to folder and get its name
                            string imgName = ImgDir + "/" + await _fileUploadService.UploadFile(item, ImgDir, ImgType);

                            //save filename to database
                            ProductImage productImage = new()
                            {
                                ProductID = id,
                                ImageURL = imgName
                            };
                            _context.ProductImage.Add(productImage);
                        }
                    }
                    //update information
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.ProductID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryDescription", products.CategoryID);
            ViewData["PlayerID"] = new SelectList(_context.Player, "PlayerID", "FullName", products.PlayerID);
            ViewData["TeamID"] = new SelectList(_context.Team, "TeamID", "FullName", products.TeamID);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Player)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'SoccerContext.Products'  is null.");
            }

            //get current product
            var products = await _context.Products.FindAsync(id);

            if (products != null)
            {
                //get images list
                var images = await _context.ProductImage.Where(pi => pi.ProductID == products.ProductID).ToListAsync();

                //delete images on local folder and database
                foreach (var img in images)
                {
                    await _fileUploadService.DeleteFile(img.ImageURL);
                    _context.ProductImage.Remove(img);
                }

                //delete product information
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductID == id)).GetValueOrDefault();
        }
    }
}
