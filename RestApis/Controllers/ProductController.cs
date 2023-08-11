using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApis.Data;
using RestApis.Exception_Handler;
using RestApis.Models;
using RestApis.Repository;
using RestApis.UnitofWork;

namespace RestApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [ApiExceptionHandler]
    public class ProductController : Controller
    {
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Product> productRepository;

        public ProductController(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            productRepository = new ProductRepository(_unitOfWork,"crud3");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await productRepository.Get();
            return products;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            var products = await productRepository.Create(product);
            return products;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var products = await productRepository.Delete(id);
            return products;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var products = await productRepository.Update(id, product);
            return products;
        }
    }
}
