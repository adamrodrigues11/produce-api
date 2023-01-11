using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day02Exercises.Models;

namespace Day02Exercises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduceSupplierController : ControllerBase
    {
        private readonly ProduceDBContext _context;

        public ProduceSupplierController(ProduceDBContext context)
        {
            _context = context;
        }

        // GET: api/ProduceSupplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduceSupplier>>> GetProduceSuppliers()
        {
            return await _context.ProduceSuppliers.ToListAsync();
        }

        // GET: api/ProduceSupplier/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProduceSupplier>> GetProduceSupplier(int id)
        {
            var produceSupplier = await _context.ProduceSuppliers.FindAsync(id);

            if (produceSupplier == null)
            {
                return NotFound();
            }

            return produceSupplier;
        }

        // PUT: api/ProduceSupplier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduceSupplier(int id, ProduceSupplier produceSupplier)
        {
            if (id != produceSupplier.ProduceID)
            {
                return BadRequest();
            }

            _context.Entry(produceSupplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduceSupplierExists(id))
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

        // POST: api/ProduceSupplier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProduceSupplier>> PostProduceSupplier(ProduceSupplier produceSupplier)
        {
            _context.ProduceSuppliers.Add(produceSupplier);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProduceSupplierExists(produceSupplier.ProduceID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduceSupplier", new { id = produceSupplier.ProduceID }, produceSupplier);
        }

        // DELETE: api/ProduceSupplier/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduceSupplier(int id)
        {
            var produceSupplier = await _context.ProduceSuppliers.FindAsync(id);
            if (produceSupplier == null)
            {
                return NotFound();
            }

            _context.ProduceSuppliers.Remove(produceSupplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduceSupplierExists(int id)
        {
            return _context.ProduceSuppliers.Any(e => e.ProduceID == id);
        }
    }
}
