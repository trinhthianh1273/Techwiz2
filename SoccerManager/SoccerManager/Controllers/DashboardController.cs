using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.IRepository;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    public class DashboardController : Controller
    {
        private readonly SoccerContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderRepository _orderRepository;

        public DashboardController(SoccerContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        // GET: Dashboard
        public async Task<IActionResult> Index()
        {
            return View(_orderRepository.GetAllResponse());
        }

        // GET: Dashboard/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.PaymentMethod)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Dashboard/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Password");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Email");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId");
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName");
            return View();
        }

        // POST: Dashboard/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,CustomerId,EmployeeId,AddressId,OrderDate,StatusId,ShippedDate,PaymentMethodId,CardName,CardNumber,Expire,SecurityCode,PaymentStatus,PaymentDate")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1", orders.AddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Password", orders.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Email", orders.EmployeeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", orders.PaymentMethodId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", orders.StatusId);
            return View(orders);
        }

        // GET: Dashboard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1", orders.AddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Password", orders.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Email", orders.EmployeeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", orders.PaymentMethodId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", orders.StatusId);
            return View(orders);
        }

        // POST: Dashboard/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,EmployeeId,AddressId,OrderDate,StatusId,ShippedDate,PaymentMethodId,CardName,CardNumber,Expire,SecurityCode,PaymentStatus,PaymentDate")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1", orders.AddressId);
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Password", orders.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "Email", orders.EmployeeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethodId", orders.PaymentMethodId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", orders.StatusId);
            return View(orders);
        }

        // GET: Dashboard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Address)
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.PaymentMethod)
                .Include(o => o.Status)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'SoccerContext.Orders'  is null.");
            }
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Orders.Remove(orders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
