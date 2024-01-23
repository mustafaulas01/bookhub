
using System.Globalization;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? filterOn=null,string? filterQuery=null,string?sortBy=null
        ,bool isAscending=true,int pageNumber=1,int pageSize=4)
        {
            var products=  _context.Products.Include(c=>c.Category).Include(p=>p.Publisher).AsQueryable();

            //var name=product.Name.ToUpper(CultureInfo.GetCultureInfo("tr-TR"));

            if (!string.IsNullOrWhiteSpace(filterQuery))
                filterQuery = filterQuery.ToUpper(); ;
            //Filtering
            if (string.IsNullOrWhiteSpace(filterOn)==false&&string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                if (filterOn.Equals("Name"))
                {
                    products = products.Where(a => a.Name.ToUpper().Contains(filterQuery) );
                            
                }

                else if (filterOn.Equals("Description"))
                {
                    products = products.Where(a => a.Description.ToUpper().Contains(filterQuery));
                }

                else if (filterOn.Equals("Category"))
                {
                    products = products.Where(a => a.Category.Name.ToUpper().Contains(filterQuery));
                }
                else if (filterOn.Equals("Writer"))
                {
                    products = products.Where(a => a.Writer.ToUpper().Contains(filterQuery));
                }
                else if (filterOn.Equals("Publisher"))
                {
                    products = products.Where(a => a.Publisher.Name.ToUpper().Contains(filterQuery));
                }

            }
            //sorting
            if(string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if (sortBy.Equals("Name"))
                {
                    products = isAscending ? products.OrderBy(a => a.Name) : products.OrderByDescending(a => a.Name);
                }
                else if (sortBy.Equals("Category"))
                {
                    products = isAscending ? products.OrderBy(a => a.Category.Name) : products.OrderByDescending(a => a.Category.Name);
                }
                else if (sortBy.Equals("Publisher"))
                {
                    products = isAscending ? products.OrderBy(a => a.Publisher.Name) : products.OrderByDescending(a => a.Publisher.Name);
                }
                else if (sortBy.Equals("Writer"))
                {
                    products = isAscending ? products.OrderBy(a => a.Writer) : products.OrderByDescending(a => a.Writer);
                }
            }

            //pagination
            var skipResults=(pageNumber-1)*pageSize;


            return await products.Skip(skipResults).Take(pageSize).ToListAsync();
        }

    
    }
}