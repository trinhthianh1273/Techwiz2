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
    public class OrderContentsController : Controller
    {
        private readonly SoccerContext _context;

        public OrderContentsController(SoccerContext context)
        {
            _context = context;
        }

        // GET: OrderContents
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.OrderContent.Include(o => o.Order).Include(o => o.Product);
            return View(await soccerContext.ToListAsync());
        }

        // GET: OrderContents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderContent == null)
            {
                return NotFound();
            }

            var orderContent = await _context.OrderContent
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderContentId == id);
            if (orderContent == null)
            {
                return NotFound();
            }

            return View(orderContent);
        }

        // GET: OrderContents/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId");
            return View();
        }

        // POST: OrderContents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderContentId,OrderId,ProductId,Quantity,Price")] OrderContent orderContent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderContent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderContent.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderContent.ProductId);
            return View(orderContent);
        }

        // GET: OrderContents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderContent == null)
            {
                return NotFound();
            }

            var orderContent = await _context.OrderContent.FindAsync(id);
            if (orderContent == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderContent.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderContent.ProductId);
            return View(orderContent);
        }

        // POST: OrderContents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderContentId,OrderId,ProductId,Quantity,Price")] OrderContent orderContent)
        {
            if (id != orderContent.OrderContentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderContent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderContentExists(orderContent.OrderContentId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderContent.OrderId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductId", orderContent.ProductId);
            return View(orderContent);
        }

        // GET: OrderContents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderContent == null)
            {
                return NotFound();
            }

            var orderContent = await _context.OrderContent
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.OrderContentId == id);
            if (orderContent == null)
            {
                return NotFound();
            }

            return View(orderContent);
        }

        // POST: OrderContents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderContent == null)
            {
                return Problem("Entity set 'SoccerContext.OrderContent'  is null.");
            }
            var orderContent = await _context.OrderContent.FindAsync(id);
            if (orderContent != null)
            {
                _context.OrderContent.Remove(orderContent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderContentExists(int id)
        {
          return (_context.OrderContent?.Any(e => e.OrderContentId == id)).GetValueOrDefault();
        }
    }
}
