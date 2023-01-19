using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Day02Exercises.Models;
using NuGet.Protocol;

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
            return Ok(await _produceRepo.GetProduceList());
        }

        // GET: api/Produce/5
        [HttpGet("{produceID}")]
        public async Task<ActionResult<Produce>> GetProduce(int produceID)
        {
            Produce? produce = await _produceRepo.GetProduce(produceID);
            if (produce == null)
            {
                return NotFound(produceID);
            }
            return Ok(produce);
        }

        // PUT: api/Produce/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutProduce(Produce produce)
        {
            int statusCode = await _produceRepo.UpdateProduce(produce);
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
            int statusCode = await _produceRepo.AddProduce(produce);
            switch (statusCode) {
                case 200:
                    return Ok(produce);
                default:
                    return BadRequest(produce);
            }
        }

        // DELETE: api/Produce/5
        [HttpDelete("{produceID}")]
        public async Task<IActionResult> DeleteProduce(int produceID)
        {
            int statusCode = await _produceRepo.DeleteProduce(produceID);
            switch (statusCode) {
                case 200:
                    return Ok(produceID);
                case 404:
                    return NotFound(produceID);
                default:
                    return BadRequest(produceID);
            }
        }
    }
}
