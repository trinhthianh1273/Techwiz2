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
    public class ProductImagesController : ControllerBase
    {
        private readonly SoccerContext _context;

        public ProductImagesController(SoccerContext context)
        {
            _context = context;
        }

        // GET: api/ProductImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImage>>> GetProductImage()
        {
          if (_context.ProductImage == null)
          {
              return NotFound();
          }
            return await _context.ProductImage.ToListAsync();
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImage>> GetProductImage(int id)
        {
          if (_context.ProductImage == null)
          {
              return NotFound();
          }
            var productImage = await _context.ProductImage.FindAsync(id);

            if (productImage == null)
            {
                return NotFound();
            }

            return productImage;
        }

        // PUT: api/ProductImages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductImage(int id, ProductImage productImage)
        {
            if (id != productImage.ProductImageID)
            {
                return BadRequest();
            }

            _context.Entry(productImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImageExists(id))
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

        // POST: api/ProductImages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductImage>> PostProductImage(ProductImage productImage)
        {
          if (_context.ProductImage == null)
          {
              return Problem("Entity set 'SoccerContext.ProductImage'  is null.");
          }
            _context.ProductImage.Add(productImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductImage", new { id = productImage.ProductImageID }, productImage);
        }

        // DELETE: api/ProductImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductImage(int id)
        {
            if (_context.ProductImage == null)
            {
                return NotFound();
            }
            var productImage = await _context.ProductImage.FindAsync(id);
            if (productImage == null)
            {
                return NotFound();
            }

            _context.ProductImage.Remove(productImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductImageExists(int id)
        {
            return (_context.ProductImage?.Any(e => e.ProductImageID == id)).GetValueOrDefault();
        }
    }
}
