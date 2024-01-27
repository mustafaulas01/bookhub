
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
//int categoryId=0,int publisherId=0,[FromQuery]string? sort, [FromQuery]bool ? isAscending=true,[FromQuery] int pageNumber=1,[FromQuery]int pageSize=4,[FromQuery] string?search=null 
        public async Task <ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(int categoryId=0,int publisherId=0,
        [FromQuery] string? sort=null,[FromQuery]bool ? isAscending=true,[FromQuery] int pageNumber=1,[FromQuery]int pageSize=4,[FromQuery]string? search=null)
        {

//int categoryId=0,int publisherId=0,[FromQuery]string? sort, [FromQuery]bool ? isAscending=true,[FromQuery] int pageNumber=1,[FromQuery]int pageSize=4,[FromQuery] string?search=null 
//categoryId?:number,publisherId?:number,sort?:string,pageNumber:number=1,pageSize:number=4,search?:string

            var products= await _productRepository.GetProductsAsync(categoryId,publisherId,sort,isAscending,pageNumber,pageSize,search);
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