using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
         private readonly StoreContext _context;
        public CategoryRepository(StoreContext context)
        {
            _context=context;
        }
        public async Task<IReadOnlyList<Category>> GetAllCategorySync()
        {
          return await _context.Categories.ToListAsync();
        }
    }
}