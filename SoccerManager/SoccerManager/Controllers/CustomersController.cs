using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Helper;
using SoccerManager.Models;

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
            var customers = _context.Customer;
            foreach (var item in customers)
            {
                item.Address = _context.Address.Where(a => a.CustomerId == item.CustomerId).ToList();
                item.Cart = _context.Cart.Where(c => c.CustomerId == item.CustomerId).ToList();
                item.Orders = _context.Orders.Where(c => c.CustomerId == item.CustomerId).ToList();
            }

            return View(customers);
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
            double? totalAmout = 0;
            if (HttpContext.Session.GetString("CustomerId") == null)
			{
                return RedirectToAction("Login", "Customers");
            } else

            {
                int customerId = Convert.ToInt16(HttpContext.Session.GetString("CustomerId"));
                carts = _context.Cart
                                .Where(c => c.CustomerId == customerId)
                                .Include(c => c.Product)
                                .ToList();

                foreach(Cart item in carts)
                {
                    item.Product.Category = _context.Category.Where(c => c.CategoryId == item.Product.CategoryId).Single();
                    item.Product.ProductImage = _context.ProductImage.Where(p => p.ProductId == item.Product.ProductId).ToList();
                    totalAmout += item.Quantity * item.Product.Price;
                }
            }
            
            ViewBag.Carts = carts;
            ViewBag.TotalAmount = totalAmout;
            return View();
		}


        // POST: Customers/Order
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut([FromForm] List<Cart> carts)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(carts);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return View(carts);
            }
            return View(carts);
        }


        //POST: Customers/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Customer obj)
        {
            if (ModelState.IsValid)
            {
                
                var user = _context.Customer.FirstOrDefault(x => x.Username == obj.Username);
                if (user == null) return RedirectToAction("Login", "Customers");
				if (!PasswordHasher.VerifyPassword(obj.Password, user.Password))
                {
					return RedirectToAction("Login", "Customers");
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

		// GET: Customers/Details/5
		public async Task<IActionResult> Profile(int? id)
		{
			if (HttpContext.Session.GetString("CustomerId") == null)
			{
				return RedirectToAction("Login", "Customers");
			}
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


		// POST: Customers/Profile/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Profile(int id, [Bind("CustomerId,Username,Password,Fullname,Email,Phone,Token")] Customer customer)
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
			return RedirectToAction("Index", "Home");
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
                customer.Password = PasswordHasher.HassPassword(customer.Password);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut()
        {
            var Req = Request.Form;

            if (Req != null)
            {
                //get all user data
                int paymentMethod = Int32.Parse(Req["paymentMethod"].ToString());

                int address = Int32.Parse(Req["address"].ToString());
                int customerId = Int32.Parse(Req["customerId"].ToString());

                //randomize employee id
                int[] emps = _context.Employee.Select(e => e.EmployeeId).ToArray();
                int employeeId = emps[new Random().Next(0, emps.Length)];

                //add to order table
                Orders o = new Orders()
                {
                    CustomerId = customerId,
                    EmployeeId = employeeId,
                    AddressId = address,
                    StatusId = 1,
                    PaymentMethodId = paymentMethod,
                    CardName = Req["cardName"].ToString() == null ? null : Req["cardName"].ToString(),
                    CardNumber = Req["cardNumber"].ToString() == null ? null : Req["cardNumber"].ToString(),
                    Expire = Req["expire"].ToString() == null ? null : Req["expire"].ToString(),
                    SecurityCode = Int32.Parse(Req["code"].ToString()) == null ? null : Int32.Parse(Req["code"].ToString()),
                    PaymentStatus = paymentMethod == 1 ? 0 : 1,
                    OrderDate = DateTime.Now
                };

                _context.Orders.Add(o);
                await _context.SaveChangesAsync();

                //get orderid
                int orderId = o.OrderId;

                //get cart content
                var cartContent = _context.Cart.Where(c=>c.CustomerId == customerId).ToList();

                //add cart content to order content then delete from cart
                foreach (var cart in cartContent)
                {
                    OrderContent oc = new OrderContent()
                    {
                        OrderId = orderId,
                        ProductId = cart.ProductId,
                        Quantity = cart.Quantity,
                        Price = cart.Quantity * _context.Products.Where(a => a.ProductId == cart.ProductId).Single().Price,
                    };

                    _context.OrderContent.Add(oc);
                    _context.Cart.Remove(cart);

                    await _context.SaveChangesAsync();

                }

                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(Cart());
            }

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
