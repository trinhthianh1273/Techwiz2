using Api.DTO.Request;
using Api.Helpers;
using Api.IService;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly SoccerContext _context;

        private readonly IAuthenticationService _service;

        public EmployeesController(SoccerContext context, IAuthenticationService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Employees
        // List of Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        // Employee detail
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // Edit Employee
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO userObj)
        {
            if (userObj == null) return BadRequest();

            // check username
            if (await this._service.CheckUserNameExistAsync(userObj.Username))
            {
                return BadRequest(new { Message = "Username Already Exist!" });
            }

            // Check email
            if (await this._service.CheckEmailExistAsync(userObj.Email))
            {
                return BadRequest(new { Message = "Email Already Exist!" });
            }

            userObj.Password = PasswordHasher.HassPassword(userObj.Password);
            //userObj.Role = "User";
            userObj.Token = "";

            var newCustomer = new Customer()
            {
                Fullname = userObj.Fullname,
                Username = userObj.Username,
                Password = userObj.Password,
                Email = userObj.Email,
                Phone = userObj.Phone,
                Token = userObj.Token
            };

            await _context.Customer.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Customer Registered!"
            });
        }


        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.EmployeeID == id)).GetValueOrDefault();
        }
    }
}
