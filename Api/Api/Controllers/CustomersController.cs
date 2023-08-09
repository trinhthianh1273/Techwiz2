using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.DTO.Request;
using Api.Helpers;
using Api.DTO.Common;
using Api.IService;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly SoccerContext _context;
        private readonly ICustomerAuthenticationService _service;

        public CustomersController(SoccerContext context, ICustomerAuthenticationService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
          if (_context.Customer == null)
          {
              return NotFound();
          }
            return await _context.Customer.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
          if (_context.Customer == null)
          {
              return NotFound();
          }
            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
          if (_context.Customer == null)
          {
              return Problem("Entity set 'SoccerContext.Customer'  is null.");
          }
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (_context.Customer == null)
            {
                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return (_context.Customer?.Any(e => e.CustomerID == id)).GetValueOrDefault();
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto userObj)
        {
            var check = userObj == null;
            if (userObj == null) return BadRequest();
            var user = await _context.Customer.FirstOrDefaultAsync(x => x.Username == userObj.UserName);
            if (user == null) return NotFound(new { Message = "User Not Found" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                return BadRequest(new { Message = "Password is Incorredt!" });
            }
            
            user.Token = this._service.CreateJwt(user);

            user.Token = this._service.CreateJwt(user);
           
            var newAccessToken = user.Token;
            var newRefreshToken = this._service.CreateRefreshToken();
            //user.RefreshToken = newRefreshToken;
            
            await _context.SaveChangesAsync();

            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken
                //RefeshToken = newRefreshToken
            });
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
                Fullname = userObj.Fullname ,
                Username = userObj.Username ,
                Password = userObj.Password ,
                Email = userObj.Email ,
                Phone = userObj.Phone ,
                Token = userObj.Token
            };

            await _context.Customer.AddAsync(newCustomer);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Message = "Customer Registered!"
            });
        }
    }
}
