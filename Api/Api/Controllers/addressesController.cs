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
    public class AddressesController : ControllerBase
    {
        private readonly SoccerContext _context;

        public AddressesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
          if (_context.Address == null)
          {
              return NotFound();
          }
            return await _context.Address.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
          if (_context.Address == null)
          {
              return NotFound();
          }
            var Address = await _context.Address.FindAsync(id);

            if (Address == null)
            {
                return NotFound();
            }

            return Address;
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(int id, Address Address)
        {
            if (id != Address.AddressID)
            {
                return BadRequest();
            }

            _context.Entry(Address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address Address)
        {
          if (_context.Address == null)
          {
              return Problem("Entity set 'SoccerContext.Address'  is null.");
          }
            _context.Address.Add(Address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = Address.AddressID }, Address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            if (_context.Address == null)
            {
                return NotFound();
            }
            var Address = await _context.Address.FindAsync(id);
            if (Address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(Address);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressExists(int id)
        {
            return (_context.Address?.Any(e => e.AddressID == id)).GetValueOrDefault();
        }
    }
}
