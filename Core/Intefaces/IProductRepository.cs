
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Intefaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(string?filterOn=null,string? filterQuery=null,string?sortBy=null,bool isAscending=true,int pageNumber=1,int pageSize=4,int categoryId=0,int publisherId=0);

    }
}