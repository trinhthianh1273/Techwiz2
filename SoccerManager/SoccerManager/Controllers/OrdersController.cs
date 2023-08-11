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
                order.OrderContent = _context.OrderContent.Where(o => o.OrderID == order.OrderID).ToList();
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
            ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "Address1");
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Fullname");
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "FullName");
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentMethod, "PaymentMethodID", "PaymentMethod1");
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName");
            
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,CustomerID,EmployeeID,AddressID,OrderDate,StatusID,ShippedDate,PaymentMethodID,CardName,CardNumber,Expire,SecurityCode,PaymentStatus,PaymentDate")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "Address1", orders.AddressID);
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Fullname", orders.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "FullName", orders.EmployeeID);
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentMethod, "PaymentMethodID", "PaymentMethod1", orders.PaymentMethodID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName", orders.StatusID);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = _orderRepository.GetResponseById(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "Address1", orders.AddressID);
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Fullname", orders.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "FullName", orders.EmployeeID);
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentMethod, "PaymentMethodID", "PaymentMethod1", orders.PaymentMethodID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName", orders.StatusID);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,CustomerID,EmployeeID,AddressID,OrderDate,StatusID,ShippedDate,PaymentMethodID,CardName,CardNumber,Expire,SecurityCode,PaymentStatus,PaymentDate")] Orders orders, List<OrderContent> orderContents)
        {
            if (id != orders.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    _context.Update(orderContents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderID))
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
            ViewData["AddressID"] = new SelectList(_context.Address, "AddressID", "Address1", orders.AddressID);
            ViewData["CustomerID"] = new SelectList(_context.Customer, "CustomerID", "Password", orders.CustomerID);
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "Email", orders.EmployeeID);
            ViewData["PaymentMethodID"] = new SelectList(_context.PaymentMethod, "PaymentMethodID", "PaymentMethodID", orders.PaymentMethodID);
            ViewData["StatusID"] = new SelectList(_context.Status, "StatusID", "StatusName", orders.StatusID);
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
                .FirstOrDefaultAsync(m => m.OrderID == id);
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
          return (_context.Orders?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }
    }
}
