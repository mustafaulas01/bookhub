
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

        public async Task<int> GetProductCount()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(
       int categoryId=0,int publisherId=0,string?sortBy=null,bool? isAscending=true,int pageNumber=1,int pageSize=4,string?search=null)
        {
            var products=  _context.Products.Include(c=>c.Category).Include(p=>p.Publisher).AsQueryable();

            //var name=product.Name.ToUpper(CultureInfo.GetCultureInfo("tr-TR"));

            // if (!string.IsNullOrWhiteSpace(filterQuery))
            //     filterQuery = filterQuery.ToUpper(); 
            if(!string.IsNullOrEmpty(search))
            search=search.ToUpper();
            bool isAsc = isAscending?? false;

            //Filtering
            if (string.IsNullOrWhiteSpace(search)==false)
            {
              
                    products = products.Where(a => a.Name.ToUpper().Contains(search));
                            
                

                // else if (filterOn.Equals("Description"))
                // {
                //     products = products.Where(a => a.Description.ToUpper().Contains(filterQuery));
                // }

                // else if (filterOn.Equals("Category"))
                // {
                //     products = products.Where(a => a.Category.Name.ToUpper().Contains(filterQuery));
                // }
                // else if (filterOn.Equals("Writer"))
                // {
                //     products = products.Where(a => a.Writer.ToUpper().Contains(filterQuery));
                // }
                // else if (filterOn.Equals("Publisher"))
                // {
                //     products = products.Where(a => a.Publisher.Name.ToUpper().Contains(filterQuery));
                // }

            }
            //sorting
            if(string.IsNullOrWhiteSpace(sortBy)==false)
            {
                if (sortBy.Equals("Name"))
                {
                    products = isAsc ? products.OrderBy(a => a.Name) : products.OrderByDescending(a => a.Name);
                }
                else if (sortBy.Equals("priceAsc"))
                {
                    products = products.OrderBy(a => a.Price);
                }
                else if (sortBy.Equals("priceDesc"))
                {
                   products= products.OrderByDescending(a => a.Price);
              
                }

                else if (sortBy.Equals("Category"))
                {
                    products = isAsc? products.OrderBy(a => a.Category.Name) : products.OrderByDescending(a => a.Category.Name);
                }
                else if (sortBy.Equals("Publisher"))
                {
                    products = isAsc ? products.OrderBy(a => a.Publisher.Name) : products.OrderByDescending(a => a.Publisher.Name);
                }
                else if (sortBy.Equals("Writer"))
                {
                    products = isAsc? products.OrderBy(a => a.Writer) : products.OrderByDescending(a => a.Writer);
                }
            }
            //category
            if(categoryId!=0)
            products=products.Where(a=>a.CategoryId==categoryId);
            //publisher
            if(publisherId!=0)
            products=products.Where(a=>a.PublisherId==publisherId);

            //pagination
            var skipResults=(pageNumber-1)*pageSize;


            return await products.Skip(skipResults).Take(pageSize).ToListAsync();
        }

    
    }
}