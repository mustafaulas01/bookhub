
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Intefaces;
using AutoMapper;
using API.Dto;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {

       private readonly IProductRepository _productRepository;
       private readonly IMapper _mapper;
       public ProductsController(IProductRepository repository,IMapper mapper)
       {
        _productRepository=repository;
        _mapper=mapper;
       }

        [HttpGet]
        public async Task <ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var products= await _productRepository.GetProductsAsync();
           
   
          return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
        }
             
        [HttpGet("{id}")]
        public async Task < ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
          var product= await _productRepository.GetProductByIdAsync(id);
       
          return Ok(_mapper.Map<Product,ProductToReturnDto>(product));
        }
        
    }
}