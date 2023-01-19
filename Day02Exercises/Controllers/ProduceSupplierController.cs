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
        private readonly ProduceSupplierRepo _repo;
        private readonly ProduceDBContext _context;

        public ProduceSupplierController(ProduceDBContext context)
        {
            _repo = new ProduceSupplierRepo(context);
            _context = context;
        }

        // GET: api/ProduceSupplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProduceSupplier>>> GetProduceSupplierList()
        {
            return Ok(await _repo.GetProduceSupplierList());
        }

        // GET: api/ProduceSupplier/5
        [HttpGet("{produceID}/{supplierID}")]
        public async Task<ActionResult<ProduceSupplier>> GetProduceSupplier(int produceID, int supplierID)
        {
            ProduceSupplier? produceSupplier = await _repo.GetProduceSupplier(produceID, supplierID);

            if (produceSupplier == null)
            {
                return NotFound(new { produceID, supplierID });
            }

            return Ok(produceSupplier);
        }

        // PUT: api/ProduceSupplier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProduceSupplier(ProduceSupplier produceSupplier)
        {
            int statusCode = await _repo.UpdateProduceSupplier(produceSupplier);
            switch (statusCode)
            {
                case 200:
                    return Ok(produceSupplier);
                case 404:
                    return NotFound(produceSupplier);
                default:
                    return BadRequest(produceSupplier);
            }

        }

        // POST: api/ProduceSupplier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProduceSupplier>> PostProduceSupplier(ProduceSupplier produceSupplier)
        {
            int statusCode = await _repo.AddProduceSupplier(produceSupplier);
            switch (statusCode) {
                case 200:
                    return Ok(produceSupplier);
                default:
                    return BadRequest(produceSupplier);
            }            
        }

        // DELETE: api/ProduceSupplier/5
        [HttpDelete("{produceID}/{supplierID}")]
        public async Task<IActionResult> DeleteProduceSupplier(int produceID, int supplierID)
        {
            int statusCode = await _repo.DeleteProduceSupplier(produceID, supplierID);
            switch (statusCode) {
                case 200:
                    return Ok(new {produceID, supplierID});
                case 404:
                    return NotFound(new { produceID, supplierID });
                default:
                    return BadRequest(new { produceID, supplierID });
            }
        }
    }
}
