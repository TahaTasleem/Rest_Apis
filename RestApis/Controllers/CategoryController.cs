using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestApis.Exception_Handler;
using RestApis.Models;
using RestApis.Repository;
using RestApis.UnitofWork;

namespace RestApis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    [ApiExceptionHandler]
    public class CategoryController : Controller
    {
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Category> categoryRepository;
           
         public CategoryController(IUnitOfwork unitOfwork)
         {
            _unitOfWork = unitOfwork;
             categoryRepository = new CategoryRepository(_unitOfWork,"crud");
         }
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
         { 
             var categories = await categoryRepository.Get();
             return categories;
         }

         [HttpPost]
         public async Task<ActionResult<Category>> CreateCategory(Category category)
         {
             var categories = await categoryRepository.Create(category);
             return categories;
         }

         [HttpDelete("{id}")]
         public async Task<IActionResult> DeleteCategory(int id)
         {
             var categories = await categoryRepository.Delete(id);
             return categories;
         }

         [HttpPut("{id}")]
         public async Task<IActionResult> UpdateCategory(int id, Category category)
         {
             var categories = await categoryRepository.Update(id,category);
             return categories;
         }
    }
}
