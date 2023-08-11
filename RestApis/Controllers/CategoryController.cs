﻿using Microsoft.AspNetCore.Authorization;
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
             categoryRepository = new CategoryRepository(_unitOfWork);
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
        /*
        //Get Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Category.ToListAsync();
            return Ok(categories);
        }

        //Post Request
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            _context.Category.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }

        //Put Request
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var existingCategory = await _context.Category.FindAsync(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            _context.Entry(existingCategory).CurrentValues.SetValues(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        //Delete Request
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/
    }
}
