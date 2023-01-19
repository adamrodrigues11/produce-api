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
    public class SupplierController : ControllerBase
    {
        private readonly SupplierRepo _supplierRepo;

        public SupplierController(ProduceDBContext context)
        {
            _supplierRepo = new SupplierRepo(context);
        }

        // GET: api/Supplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSupplierList()
        {
            return Ok(await _supplierRepo.GetSupplierList());
        }

        // GET: api/Supplier/5
        [HttpGet("{supplierID}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int supplierID)
        {
            Supplier? supplier = await _supplierRepo.GetSupplier(supplierID);
            if (supplier == null)
            {
                return NotFound(supplierID);
            }
            return Ok(supplier);
        }

        // PUT: api/Supplier/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSupplier(Supplier supplier)
        {
            int statusCode = await _supplierRepo.UpdateSupplier(supplier);
            switch (statusCode)
            {
                case 200:
                    return Ok(supplier);
                case 404:
                    return NotFound(supplier);
                default:
                    return BadRequest(supplier);
            }
        }

        // POST: api/Supplier
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            int statusCode = await _supplierRepo.AddSupplier(supplier);
            switch (statusCode)
            {
                case 200:
                    return Ok(supplier);
                default:
                    return BadRequest(supplier);
            }
        }

        // DELETE: api/Supplier/5
        [HttpDelete("{supplierID}")]
        public async Task<IActionResult> DeleteSupplier(int supplierID)
        {
            int statusCode = await _supplierRepo.DeleteSupplier(supplierID);
            switch (statusCode)
            {
                case 200:
                    return Ok(supplierID);
                case 404:
                    return NotFound(supplierID);
                default:
                    return BadRequest(supplierID);
            }
        }
    }
}
