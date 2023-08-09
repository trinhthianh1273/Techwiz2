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
    public class OrderContentsController : ControllerBase
    {
        private readonly SoccerContext _context;

        public OrderContentsController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/OrderContents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderContent>>> GetOrderContent()
        {
          if (_context.OrderContent == null)
          {
              return NotFound();
          }
            return await _context.OrderContent.ToListAsync();
        }

        // GET: api/OrderContents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderContent>> GetOrderContent(int id)
        {
          if (_context.OrderContent == null)
          {
              return NotFound();
          }
            var orderContent = await _context.OrderContent.FindAsync(id);

            if (orderContent == null)
            {
                return NotFound();
            }

            return orderContent;
        }

        // PUT: api/OrderContents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderContent(int id, OrderContent orderContent)
        {
            if (id != orderContent.OrderContentID)
            {
                return BadRequest();
            }

            _context.Entry(orderContent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderContentExists(id))
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

        // POST: api/OrderContents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderContent>> PostOrderContent(OrderContent orderContent)
        {
          if (_context.OrderContent == null)
          {
              return Problem("Entity set 'SoccerContext.OrderContent'  is null.");
          }
            _context.OrderContent.Add(orderContent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderContent", new { id = orderContent.OrderContentID }, orderContent);
        }

        // DELETE: api/OrderContents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderContent(int id)
        {
            if (_context.OrderContent == null)
            {
                return NotFound();
            }
            var orderContent = await _context.OrderContent.FindAsync(id);
            if (orderContent == null)
            {
                return NotFound();
            }

            _context.OrderContent.Remove(orderContent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderContentExists(int id)
        {
            return (_context.OrderContent?.Any(e => e.OrderContentID == id)).GetValueOrDefault();
        }
    }
}
