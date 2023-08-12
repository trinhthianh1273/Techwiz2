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
    public class PaymentMethodsController : Controller
    {
        private readonly SoccerContext _context;

        public PaymentMethodsController(SoccerContext context)
        {
            _context = context;
        }

        // GET: PaymentMethods
        public async Task<IActionResult> Index()
        {
            var paymentMethods = _context.PaymentMethod;
            foreach(PaymentMethod item in paymentMethods)
            {
                item.Orders = _context.Orders.Where(o => o.PaymentMethodId == item.PaymentMethodId).ToList();
            }
            return View(paymentMethods.ToList());
        }

        // GET: PaymentMethods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PaymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod
                .FirstOrDefaultAsync(m => m.PaymentMethodId == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentMethodId,PaymentMethod1")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PaymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentMethodId,PaymentMethod1")] PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.PaymentMethodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentMethodExists(paymentMethod.PaymentMethodId))
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
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PaymentMethod == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethod
                .FirstOrDefaultAsync(m => m.PaymentMethodId == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PaymentMethod == null)
            {
                return Problem("Entity set 'SoccerContext.PaymentMethod'  is null.");
            }
            var paymentMethod = await _context.PaymentMethod.FindAsync(id);
            if (paymentMethod != null)
            {
                _context.PaymentMethod.Remove(paymentMethod);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(int id)
        {
          return (_context.PaymentMethod?.Any(e => e.PaymentMethodId == id)).GetValueOrDefault();
        }
    }
}
