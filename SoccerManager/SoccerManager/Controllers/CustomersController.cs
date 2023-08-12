using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoccerManager.Models;
using System.Security.Cryptography;
using SoccerManager.Helper;

namespace SoccerManager.Controllers
{
    public class CustomersController : Controller
    {
        private readonly SoccerContext _context;

        public CustomersController(SoccerContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
              return _context.Customer != null ? 
                          View(await _context.Customer.ToListAsync()) :
                          Problem("Entity set 'SoccerContext.Customer'  is null.");
        }

        // GET: Customers/Login
        public IActionResult Login()
        {
            Customer obj = new Customer();
            if (Request.Cookies["Username"] != null)
            {
                obj.Username = Request.Cookies["Username"];                
            }
            return View(obj);
        }

		// GET: Customers/Cart
		public IActionResult Cart()
		{

            var carts  = new List<Cart>();

            if (HttpContext.Session.GetString("CustomerId") != null)
			{
				int customerId = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
				carts = _context.Cart
								.Where(c => c.CustomerId == customerId)
								.Include(c => c.Product)
								.ToList();
			}
            ViewBag.Carts = carts;

            return View(carts);
		}

		//POST: Customers/Login
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Customer obj)
        {
            if (ModelState.IsValid)
            {
                var hb = Convert.FromBase64String(obj.Password);
                // PasswordHasher.VerifyPassword(userObj.Password, user.Password)
                var user = _context.Customer.FirstOrDefault(x => x.Username == obj.Username);
                if (user == null) return Content("UserName not found!");
                if (!PasswordHasher.VerifyPassword(obj.Password, user.Password))
                {
                    return Content("Password errors!");
                }
                HttpContext.Session.SetString("CusUserName", user.Username);
                HttpContext.Session.SetString("CusFullName", user.Fullname);
                HttpContext.Session.SetString("CustomerId", user.CustomerId.ToString());

                var carts = _context.Cart
                                .Where(c => c.CustomerId == user.CustomerId)
                                .Include(c => c.Product)
                                .ToList().Count();
                HttpContext.Session.SetString("CartCount", carts.ToString());
                return RedirectToAction("Index", "Home");


                
            }
            return RedirectToAction("Login", "Customers");
        }

        // GET: Customers/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Customers/Register
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("CustomerId,Username,Password,Fullname,Email,Phone,Token")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Password = PasswordHasher.HassPassword(customer.Password);
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View(customer);
        }


        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Username,Password,Fullname,Email,Phone,Token")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,Username,Password,Fullname,Email,Phone,Token")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    customer.Password = PasswordHasher.HassPassword(customer.Password);
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerId))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }

            var customer = await _context.Customer
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Customer == null)
            {
                return Problem("Entity set 'SoccerContext.Customer'  is null.");
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null)
            {
                _context.Customer.Remove(customer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
          return (_context.Customer?.Any(e => e.CustomerId == id)).GetValueOrDefault();
        }
    }
}
