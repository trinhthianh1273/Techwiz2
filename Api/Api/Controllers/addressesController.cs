using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class addressesController : ControllerBase
    {
        private readonly SoccerContext _context;

        public addressesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<address>>> Getaddress()
        {
          if (_context.address == null)
          {
              return NotFound();
          }
            return await _context.address.ToListAsync();
        }

        // GET: api/addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<address>> Getaddress(int id)
        {
          if (_context.address == null)
          {
              return NotFound();
          }
            var address = await _context.address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // PUT: api/addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putaddress(int id, address address)
        {
            if (id != address.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!addressExists(id))
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

        // POST: api/addresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<address>> Postaddress(address address)
        {
          if (_context.address == null)
          {
              return Problem("Entity set 'SoccerContext.address'  is null.");
          }
            _context.address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getaddress", new { id = address.AddressID }, address);
        }

        // DELETE: api/addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteaddress(int id)
        {
            if (_context.address == null)
            {
                return NotFound();
            }
            var address = await _context.address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            _context.address.Remove(address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool addressExists(int id)
        {
            return (_context.address?.Any(e => e.AddressID == id)).GetValueOrDefault();
        }
    }
}
