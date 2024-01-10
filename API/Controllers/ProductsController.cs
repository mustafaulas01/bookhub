
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Intefaces;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {

       private readonly IProductRepository _productRepository;
       public ProductsController(IProductRepository repository)
       {
        _productRepository=repository;
       }

        [HttpGet]
        public async Task < ActionResult<List<Product>>> GetProducts()
        {
          var products= await _productRepository.GetProductsAsync();

          return Ok(products);
        }
             
        [HttpGet("{id}")]
        public async Task < ActionResult<Product>> GetProduct(int id)
        {
          var product= await _productRepository.GetProductByIdAsync(id);

          return Ok(product);
        }
        
    }
}