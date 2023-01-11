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
    public class ProduceController : ControllerBase
    {
        private readonly ProduceRepo _produceRepo;

        public ProduceController(ProduceDBContext context)
        {
            _produceRepo = new ProduceRepo(context);
        }

        // GET: api/Produce
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produce>>> GetProduceList()
        {
            return Ok(await _produceRepo.getProduceList());
        }

        // GET: api/Produce/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produce>> GetProduce(int id)
        {
            Produce? produce = await _produceRepo.getProduce(id);
            if (produce == null)
            {
                return NotFound(id);
            }
            return Ok(produce);
        }

        // PUT: api/Produce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduce(int id, Produce produce)
        {
            if (id != produce.ProduceID) {
                return BadRequest(produce);
            }
            int statusCode = await _produceRepo.updateProduce(produce);
            switch (statusCode) {
                case 200:
                    return Ok(produce);
                case 404:
                    return NotFound(produce);
                default:
                    return BadRequest(produce);
            }
        }

        // POST: api/Produce
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Produce>> PostProduce(Produce produce)
        {
            int statusCode = await _produceRepo.addProduce(produce);
            switch (statusCode) {
                case 201:
                    return Ok(produce);
                default:
                    return BadRequest(produce);
            }
        }

        // DELETE: api/Produce/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduce(int id)
        {
            int statusCode = await _produceRepo.deleteProduce(id);
            switch (statusCode) {
                case 200:
                    return Ok(id);
                default:
                    return BadRequest(id);
            }
        }
    }
}
