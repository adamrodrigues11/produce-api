using Day02Exercises.Models;

namespace Day02Exercises.Repos
{
    public class SupplierRepo
    {
        private readonly ProduceDBContext _context;

        public SupplierRepo(ProduceDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSupplierList()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetSupplier(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<int> UpdateSupplier(Supplier supplier)
        {
            if (!_context.Suppliers.Contains(supplier))
            {
                return 404;
            }
            _context.Entry(supplier).State = EntityState.Modified;
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

        public async Task<int> AddSupplier(Supplier supplier)
        {
            try
            {
                supplier.SupplierID = 0; // allow the database to set the id as PK
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();
                return 200;
            }
            catch
            {
                return 400;
            }
        }

        public async Task<int> DeleteSupplier(int id)
        {
            Supplier? supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return 404;
            }
            // check for children before deleting (no cascade deletes)
            if (supplier.ProduceSuppliers != null)
            {
                return 400;
            }
            try
            {
                _context.Suppliers.Remove(supplier);
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
