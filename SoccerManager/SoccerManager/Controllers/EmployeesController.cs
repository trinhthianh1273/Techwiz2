using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Helper;
using SoccerManager.Models;

namespace SoccerManager.Controllers
{
	public class EmployeesController : Controller
	{
		private readonly SoccerContext _context;

		public EmployeesController(SoccerContext context)
		{
			_context = context;
		}

		// GET: Employees
		public async Task<IActionResult> Index()
		{
			return _context.Employee != null ?
						View(await _context.Employee.ToListAsync()) :
						Problem("Entity set 'SoccerContext.Employee'  is null.");
		}

		// GET: Employees/Login
		public ActionResult Login()
		{
			Employee obj = new Employee();
			if (Request.Cookies["UserName"] != null)
			{
				obj.UserName = Request.Cookies["UserName"];
			}
			return View(obj);
		}

		//POST: Employees/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(Employee obj)
		{
			if (ModelState.IsValid)
			{
				var user = _context.Employee.FirstOrDefault(x => x.UserName == obj.UserName);
				if (user == null) return RedirectToAction("Login", "Employees");
				if (!PasswordHasher.VerifyPassword(obj.Password, user.Password))
				{
					return RedirectToAction("Login", "Employees");
				}
				HttpContext.Session.SetString("EmpUserName", user.UserName);
				HttpContext.Session.SetString("EmpFullName", user.FullName);
				HttpContext.Session.SetString("EmployeeId", user.EmployeeId.ToString());


				return RedirectToAction("Index", "Dashboard");
			}
			return RedirectToAction("Login", "Employees");
		}

		// GET: Employees/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Employee == null)
			{
				return NotFound();
			}

			var employee = await _context.Employee
				.FirstOrDefaultAsync(m => m.EmployeeId == id);
			if (employee == null)
			{
				return NotFound();
			}

			return View(employee);
		}

		// GET: Employees/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Employees/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("EmployeeId,FullName,UserName,Email,Password,Role")] Employee employee)
		{
			if (ModelState.IsValid)
			{
				employee.Password = PasswordHasher.HassPassword(employee.Password);
				_context.Add(employee);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(employee);
		}

		// GET: Employees/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Employee == null)
			{
				return NotFound();
			}


			var employee = await _context.Employee.FindAsync(id);
			if (employee == null)
			{
				return NotFound();
			}
			return View(employee);
		}

		// POST: Employees/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FullName,UserName,Email,Password,Role")] Employee employee)
		{
			if (id != employee.EmployeeId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					employee.Password = PasswordHasher.HassPassword(employee.Password);
					_context.Update(employee);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EmployeeExists(employee.EmployeeId))
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
			return View(employee);
		}

		// GET: Employees/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Employee == null)
			{
				return NotFound();
			}

			var employee = await _context.Employee
				.FirstOrDefaultAsync(m => m.EmployeeId == id);
			if (employee == null)
			{
				return NotFound();
			}

			return View(employee);
		}

		// POST: Employees/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Employee == null)
			{
				return Problem("Entity set 'SoccerContext.Employee'  is null.");
			}
			var employee = await _context.Employee.FindAsync(id);
			if (employee != null)
			{
				_context.Employee.Remove(employee);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		//GET: Employees/ManageOrder
		public async Task<IActionResult> ManageOrder()
		{
			var orders = _context.Orders
						.Include(u => u.Customer)
						.Include(a => a.Address)
						.Include(s => s.Status)
						.Include(m => m.PaymentMethod)
						.ToList();

			return View(orders);

		}

		// GET : Employees/CreateOrder
		public async Task<IActionResult> CreateOrder(int)
		{
			ViewBag.Customer = _context.Customer.ToList();
			ViewBag.Status = _context.Status.ToList();
			ViewBag.PaymentMethod = _context.PaymentMethod.ToList();
			ViewBag.Product = _context.Products.ToList();

			return View();
		}
		// POST : Employees/CreateOrder
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateOrder(string[] productList)
		{
			var Req = Request.Form;

			if (Req != null)
			{
				//get all user data
				int customerId = Int32.Parse(Req["customer"].ToString());
				int paymentMethod = Int32.Parse(Req["paymentMethod"].ToString());

				//randomize employee id
				int[] emps = _context.Employee.Select(e => e.EmployeeId).ToArray();
				int employeeId = emps[new Random().Next(0, emps.Length)];

				//add to order table
				Orders o = new Orders()
				{
					CustomerId = customerId,
					EmployeeId = employeeId,
					AddressId = 0,
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

				//add selected product to database
				foreach (var product in productList)
				{
					int pid = Int32.Parse(product);
					//check if product is exists
					var check = _context.OrderContent.Where(o => o.OrderId == orderId && o.ProductId == pid).FirstOrDefault();

					if (check != null)  //product with the same pid and orderid is exist, update qty and price
					{
						check.Quantity++;
						check.Price += _context.Products.Find(pid).Price;
						_context.OrderContent.Update(check);
						await _context.SaveChangesAsync();
					}
					else
					{
						//add
						OrderContent oc = new OrderContent()
						{
							OrderId = orderId,
							ProductId = Int32.Parse(product),
							Quantity = 1,
							Price = _context.Products.Where(a => a.ProductId == pid).Single().Price,
						};

						_context.OrderContent.Add(oc);
						await _context.SaveChangesAsync();
					}
				}
				return RedirectToAction("UpdateOrder", orderId);

			}
			else
			{
				return NotFound();
			}
		}
		// GET : Employees/UpdateOrder
		public async Task<IActionResult> UpdateOrder(int? id)
		{

			var order = _context.Orders
						.Where(o => o.OrderId == id)
						.Include(c => c.Customer)
						.Include(a => a.Address)
						.Include(s => s.Status)
						.Include(p => p.PaymentMethod)
						.Single();

			if (order == null)
			{
				return NotFound();
			}

			ViewBag.Status = _context.Status.ToList();
			ViewBag.PaymentMethod = _context.PaymentMethod.ToList();

			return View(order);

		}

		// POST : Employees/UpdateOrder
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UpdateOrder()
		{

			var req = Request.Form;

			//current order detail
			Orders co = _context.Orders.Find(Int32.Parse(req["orderId"]));

			_context.Entry(co).State = EntityState.Detached; // Detach the existing instance

			Orders o = new Orders()
			{
				OrderId = co.OrderId,
				CustomerId = co.CustomerId,
				EmployeeId = co.EmployeeId,
				AddressId = Int32.Parse(req["address"].ToString()),
				OrderDate = co.OrderDate,
				StatusId = Int32.Parse(req["status"].ToString()),
				ShippedDate = req["shippedDate"].ToString().Length == 0 ? null : DateTime.Parse(req["shippedDate"].ToString()),
				PaymentMethodId = co.PaymentMethodId,
				CardName = co.CardName,
				CardNumber = co.CardNumber,
				Expire = co.Expire,
				SecurityCode = co.SecurityCode,
				PaymentStatus = Int32.Parse(req["paymentStatus"].ToString()),
				PaymentDate = Int32.Parse(req["paymentStatus"].ToString()) == 0 ? null : DateTime.Now

			};

			_context.Orders.Update(o);
			await _context.SaveChangesAsync();


			return RedirectToAction("ManageOrder");
		}



		private bool EmployeeExists(int id)
		{
			return (_context.Employee?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
		}
	}
}
