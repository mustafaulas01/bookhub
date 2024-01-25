
using Core.Entities;
using Core.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {

        private readonly ICategoryRepository _categoryRepo;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepo = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            var categories =await _categoryRepo.GetAllCategorySync();

            if(categories.Any())
            return Ok(categories);

            return NotFound();
        }

    }
}