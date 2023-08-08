
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApis.Data;
using RestApis.Models;

namespace RestApis.Repository
{
    public abstract class RepositoryBase<T> : ControllerBase, IRepository<T> where T:class,IEntity
    {
        protected readonly DbContext _context;
        protected DbSet<T> dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }

        //Get Request
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var data = await _context.Set<T>().ToListAsync();
            return Ok(data);
        }

        //Create Request
        public async Task<ActionResult<T>> Create(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
            return entity;
        }

        //Update Request
        public async Task<IActionResult> Update(int id, T entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            var existingOrder = await _context.Set<T>().FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _context.Entry(existingOrder).CurrentValues.SetValues(entity);

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
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            _context.Set<T>().Remove(data);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
