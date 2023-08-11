using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Interfaces;
using SoccerManager.Models;
using SoccerManager.ViewModels;

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
        public async Task<IActionResult> Shopping(int ? CategoryID)
        {
            var soccerContext = new List<Products>();
			if (CategoryID==null )
            {
				soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage).ToList();
			}
            else
            {
                soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team).Include(p => p.ProductImage).Where(p => p.CategoryId == CategoryID).ToList();
			}    
			
            
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
			} else
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
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName");
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName");
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FullName");
            return View();
        }

        // POST: Products/Create
        // Handle create Product request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,CategoryId,Price,InStock,OnOrder,Discontinued,TeamId,PlayerId")] Products products, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                //add product to database
                _context.Add(products);
                await _context.SaveChangesAsync();

                //get last inserted product id
                var lastInsertedId = products.ProductId;

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
                                ProductId = lastInsertedId,
                                ImageUrl = imgName
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryName", products.CategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName", products.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FullName", products.TeamId);

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
            var products = _context.Products.Where(p => p.ProductId == id).Include(pi => pi.ProductImage).Single();

            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryDescription", products.CategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName", products.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FullName", products.TeamId);
            return View(products);
        }

        // POST: Products/Edit/5
        // Handle PUT Product Request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
                    int id,
                    [Bind("ProductId,ProductName,Description,CategoryId,Price,InStock,OnOrder,Discontinued,TeamId,PlayerId")] Products products,
                    string[] deleteImage,
                    List<IFormFile> files)
        {
            if (id != products.ProductId)
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
                            _context.Remove(_context.ProductImage.Where(p => p.ImageUrl == img).FirstOrDefault());  //delete from database
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
                                ProductId = id,
                                ImageUrl = imgName
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
                    if (!ProductsExists(products.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryDescription", products.CategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName", products.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FullName", products.TeamId);
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
                .FirstOrDefaultAsync(m => m.ProductId == id);
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
                var images = await _context.ProductImage.Where(pi => pi.ProductId == products.ProductId).ToListAsync();

                //delete images on local folder and database
                foreach (var img in images)
                {
                    await _fileUploadService.DeleteFile(img.ImageUrl);
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
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
