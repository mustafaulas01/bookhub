
using Core.Entities;
using Core.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;

        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.
           Include(c=>c.Category).Include(p=>p.Publisher).FirstOrDefaultAsync(a=>a.Id==id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
           return await _context.Products.
           Include(c=>c.Category).Include(p=>p.Publisher).
           ToListAsync();
        }
    }
}