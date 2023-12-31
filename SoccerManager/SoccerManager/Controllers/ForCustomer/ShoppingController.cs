﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers.ForCustomer
{
    public class ShoppingController : Controller
    {
        private readonly SoccerContext _context;

        public ShoppingController(SoccerContext context)
        {
            _context = context;
        }

        // GET: Shopping
        public async Task<IActionResult> Index()
        {
            var soccerContext = _context.Products.Include(p => p.Category).Include(p => p.Player).Include(p => p.Team);
            return View(await soccerContext.ToListAsync());
        }

        // GET: Shopping/Details/5
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

        // GET: Shopping/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryDescription");
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName");
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FoundedPosition");
            return View();
        }

        // POST: Shopping/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Description,CategoryId,Price,InStock,OnOrder,Discontinued,TeamId,PlayerId")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryDescription", products.CategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName", products.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FoundedPosition", products.TeamId);
            return View(products);
        }

        // GET: Shopping/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryDescription", products.CategoryId);
            ViewData["PlayerId"] = new SelectList(_context.Player, "PlayerId", "FullName", products.PlayerId);
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FoundedPosition", products.TeamId);
            return View(products);
        }

        // POST: Shopping/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Description,CategoryId,Price,InStock,OnOrder,Discontinued,TeamId,PlayerId")] Products products)
        {
            if (id != products.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["TeamId"] = new SelectList(_context.Team, "TeamId", "FoundedPosition", products.TeamId);
            return View(products);
        }

        // GET: Shopping/Delete/5
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

        // POST: Shopping/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'SoccerContext.Products'  is null.");
            }
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
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
