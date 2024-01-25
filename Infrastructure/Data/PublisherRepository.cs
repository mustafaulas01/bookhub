
using Core.Entities;
using Core.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly StoreContext _context;
        public PublisherRepository(StoreContext context)
        {
            _context=context;
        }
        public async Task<IReadOnlyList<Publisher>> GetAllPublishersSync()
        {
            return await _context.Publishers.ToListAsync();
        }
    }
}