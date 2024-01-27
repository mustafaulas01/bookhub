
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Intefaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync(
        int categoryId=0,int publisherId=0,string?sortBy=null,bool? isAscending=true,int pageNumber=1,int pageSize=4,string?search=null);

//int categoryId=0,int publisherId=0, [FromQuery] string? sortBy,[FromQuery]bool ? isAscending,[FromQuery] int pageNumber=1,[FromQuery]int pageSize=4,[FromQuery]string? search
        Task<int> GetProductCount();

    }
}