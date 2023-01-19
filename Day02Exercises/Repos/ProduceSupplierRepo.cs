using Day02Exercises.Models;

namespace Day02Exercises.Repos
{
    public class ProduceSupplierRepo
    {
        private readonly ProduceDBContext _context;

        public ProduceSupplierRepo(ProduceDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProduceSupplier>> GetProduceSupplierList()
        {
            return await _context.ProduceSuppliers.ToListAsync();
        }

        public async Task<ProduceSupplier?> GetProduceSupplier(int produceID, int supplierID)
        {
            return await _context.ProduceSuppliers.FindAsync(produceID, supplierID);
        }

        public async Task<int> UpdateProduceSupplier(ProduceSupplier produceSupplier)
        {
            if (!_context.ProduceSuppliers.Contains(produceSupplier)) {
                return 404;
            }
            _context.Entry(produceSupplier).State = EntityState.Modified;
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

        public async Task<int> AddProduceSupplier(ProduceSupplier produceSupplier)
        {
            // guard against bad requests
            // if produceSupplier entry with same composite key already exists
            bool alreadyExists = await _context.ProduceSuppliers.ContainsAsync(produceSupplier);
            if (alreadyExists)
            {
                return 400;
            }
            // if produce does not exist for the given produceID
            Produce? produceParent = await _context.Produces.FindAsync(produceSupplier.ProduceID);
            if (produceParent == null) {
                return 400; 
            }
            // if suppier does not exist for the given supplierID
            Supplier? supplierParent = await _context.Suppliers.FindAsync(produceSupplier.SupplierID);
            if (supplierParent == null) {
                return 400;
            }
            // attempt to add record to the database
            try
            {
                await _context.ProduceSuppliers.AddAsync(produceSupplier);
                await _context.SaveChangesAsync();
                return 200;
            }
            catch
            {
                return 400;
            }
        }

        public async Task<int> DeleteProduceSupplier(int produceID, int supplierID)
        {
            ProduceSupplier? produceSupplier = await _context.ProduceSuppliers.FindAsync(produceID, supplierID);
            if (produceSupplier == null)
            {
                return 404;
            }
            try
            {
                _context.ProduceSuppliers.Remove(produceSupplier);
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
