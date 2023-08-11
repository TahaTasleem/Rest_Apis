
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApis.Data;
using RestApis.Models;
using RestApis.UnitofWork;

namespace RestApis.Repository
{
    public abstract class RepositoryBase<T> : ControllerBase, IRepository<T> where T:class,IEntity
    {
        protected readonly DbContext _context;
        protected DbSet<T> dbSet;
        private readonly IUnitOfwork _unitOfWork;

        public RepositoryBase(IUnitOfwork unitOfwork)
        {
            _unitOfWork = unitOfwork;
            dbSet = _unitOfWork.Context.Set<T>();
        }

        //Get Request
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var data = await dbSet.ToListAsync();
            //var data = await _context.Set<T>().ToListAsync();
            return Ok(data);
        }

        //Create Request
        public async Task<ActionResult<T>> Create(T entity)
        {
            dbSet.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            //_context.Set<T>().Add(entity);
            //await _context.SaveChangesAsync();            
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

            var existingOrder = await dbSet.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _unitOfWork.Context.Entry(existingOrder).CurrentValues.SetValues(entity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
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
            var data = await dbSet.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            dbSet.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
