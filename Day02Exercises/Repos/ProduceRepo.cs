﻿using Day02Exercises.Models;

namespace Day02Exercises.Repos
{
    public class ProduceRepo
    {
        private readonly ProduceDBContext _context;

        public ProduceRepo(ProduceDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produce>> GetProduceList()
        {
            return await _context.Produces.ToListAsync();
        }

        public async Task<Produce?> GetProduce(int id)
        {
            return await _context.Produces.FindAsync(id);
        }

        public async Task<int> UpdateProduce(Produce produce)
        {
            if (!_context.Produces.Contains(produce)) {
                return 404;
            }
            _context.Entry(produce).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return 200;
            }
            catch
            {
                return 400;
            }
        }

        public async Task<int> AddProduce(Produce produce)
        {
            try
            {
                produce.ProduceID = 0; // allow the database to set the id as PK
                await _context.Produces.AddAsync(produce);
                await _context.SaveChangesAsync();
                return 200;
            }
            catch
            {
                return 400;
            }
        }

        public async Task<int> DeleteProduce(int id)
        {
            Produce? produce = await _context.Produces.FindAsync(id);
            if (produce == null) {
                return 404;
            }
            // check for children before deleting (no cascade deletes)
            if (produce.ProduceSuppliers != null) {
                return 400;
            }
            try
            {
                _context.Produces.Remove(produce);
                await _context.SaveChangesAsync();
                return 200;
            }
            catch
            {
                return 400;
            }
        }
    }
}
