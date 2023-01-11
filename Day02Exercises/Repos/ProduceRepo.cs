using Day02Exercises.Models;

namespace Day02Exercises.Repos
{
    public class ProduceRepo
    {
        private readonly ProduceDBContext _context;

        public ProduceRepo(ProduceDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Produce>> getProduceList()
        {
            return await _context.Produces.ToListAsync();
        }

        public async Task<Produce?> getProduce(int id)
        {
            return await _context.Produces.FindAsync(id);
        }

        public async Task<int> updateProduce(Produce produce)
        {
            _context.Entry(produce).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return 200;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduceExists(produce.ProduceID))
                {
                    return 404;
                }
                else
                {
                    return 400;
                }
            }
        }

        public async Task<int> addProduce(Produce produce)
        {
            try
            {
                produce.ProduceID = 0; // allow the database to set the id as PK
                await _context.Produces.AddAsync(produce);
                await _context.SaveChangesAsync();
                return 201;
            }
            catch
            {
                return 400;
            }
        }

        public async Task<int> deleteProduce(int id)
        {
            Produce? produce = await _context.Produces.FindAsync(id);
            if (produce == null) {
                return 404;
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

        private bool ProduceExists(int id)
        {
            return _context.Produces.Any(p => p.ProduceID == id);
        }
    }
}
