using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoccerManager.DTO.Response;
using SoccerManager.IRepository;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
    public class OrdersController : Controller
    {
        private readonly SoccerContext _context;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(SoccerContext context, IOrderRepository orderRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(_orderRepository.GetAllResponse());
            /*
            List<OrderRespone> results = new List<OrderRespone>();
            var soccerContext = _context.Orders.Include(o => o.Address).Include(o => o.Customer).Include(o => o.Employee).Include(o => o.PaymentMethod).Include(o => o.Status);
            foreach(var order in soccerContext)
            {
                order.OrderContent = _context.OrderContent.Where(o => o.OrderId == order.OrderId).ToList();
                results.Add(OrderRespone.ConvertToOrderResponse(order));
            }
            return View(results);
            */
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }
            return View(_orderRepository.GetResponseById(id));
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["AddressId"] = new SelectList(_context.Address, "AddressId", "Address1");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fullname");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FullName");
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethod1");
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName");
            return View();
        }

        // POST: Orders/Create
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fullname", orders.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FullName", orders.EmployeeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethod1", orders.PaymentMethodId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", orders.StatusId);
            return View(orders);
        }

        // GET: Orders/Edit/5
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Fullname", orders.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "EmployeeId", "FullName", orders.EmployeeId);
            ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethod, "PaymentMethodId", "PaymentMethod1", orders.PaymentMethodId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusName", orders.StatusId);
            return View(orders);
        }

        // POST: Orders/Edit/5
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

        // GET: Orders/Delete/5
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

        // POST: Orders/Delete/5
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
