
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Intefaces;
using AutoMapper;
using API.Dto;
using API.Errors;

namespace API.Controllers
{

 
    public class ProductsController:BaseApiController
    {

       private readonly IProductRepository _productRepository;
       private readonly IMapper _mapper;
       public ProductsController(IProductRepository repository,IMapper mapper)
       {
        _productRepository=repository;
        _mapper=mapper;
       }

        [HttpGet] //GET: /api/products?filterOn=Name&filterQuery=yeşiller&sortBy=Category&isAscending=true&pageNumber=1&pageSize=10
        public async Task <ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts([FromQuery] string?filterOn,[FromQuery]string?filterQuery,
        [FromQuery]bool ? isAscending,
        int categoryId=0,int publisherId=0,string? sort=null,[FromQuery] int pageNumber=1,[FromQuery]int pageSize=4 )
        {
            var products= await _productRepository.GetProductsAsync(filterOn,filterQuery,isAscending ?? true,categoryId,publisherId,sort,pageNumber,pageSize);
            var listofProducts=_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products).ToList();
            int totalCount=0;
            
            if(categoryId==0&&publisherId==0)
             totalCount=  await _productRepository.GetProductCount();
             else 
             totalCount=products.Count;

           var productResponse=new ProductResponseDto
           {
            Data=listofProducts,
            PageNumber=pageNumber,
            PageSize=pageSize,
            TotalCount=totalCount
           };
   
          return Ok(productResponse);
        }
             
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)] 
        public async Task < ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
          var product= await _productRepository.GetProductByIdAsync(id);
       
       if(product==null)
       return NotFound(new ApiResponse(404));

          return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
        
    }
}