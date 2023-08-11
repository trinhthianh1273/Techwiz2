using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoccerManager.Models;

namespace SoccerManager.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderContentsAPIController : ControllerBase
    {
        private readonly SoccerContext _context;

        public OrderContentsAPIController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/OrderContentsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderContent>>> GetOrderContent()
        {
          if (_context.OrderContent == null)
          {
              return NotFound();
          }
            return await _context.OrderContent.ToListAsync();
        }

        // GET: api/OrderContentsAPI/5
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

        // PUT: api/OrderContentsAPI/5
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

        // POST: api/OrderContentsAPI
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

        // DELETE: api/OrderContentsAPI/5
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
