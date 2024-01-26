
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Intefaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(string?filterOn=null,string? filterQuery=null,bool isAscending=true,
        int categoryId=0,int publisherId=0,string?sortBy=null,int pageNumber=1,int pageSize=4);

        Task<int> GetProductCount();

    }
}