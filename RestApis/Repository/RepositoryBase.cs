
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestApis.Data;
using RestApis.UnitofWork;

namespace RestApis.Repository
{
    public abstract class RepositoryBase<T> : ControllerBase, IRepository<T> where T:class,IEntity
    {
        protected readonly DbContext _context;
        //protected DbSet<T> dbSet;
        private readonly IUnitOfwork _unitOfWork;
        private readonly string ProcedureName;

        public RepositoryBase(IUnitOfwork unitOfwork, string crudProcedureName)
        {
            _unitOfWork = unitOfwork;
            _context = unitOfwork.Context;
            ProcedureName = crudProcedureName;
            //dbSet = _unitOfWork.Context.Set<T>();
        }
        protected string BuildSqlQuery(T entity, string choice)
        {
            var parameters = entity
                .GetType()
                .GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(StoredProcedureParameterAttribute)))
                .Select(prop =>
                {
                    var value = prop.GetValue(entity);
                    if (value is string)
                    {
                        return $"@{prop.Name}='{value}'";
                    }
                    else if (value is DateTime)
                    {
                        var formattedDate = ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
                        return $"@{prop.Name}='{formattedDate}'";
                    }
                    return $"@{prop.Name}={value}";
                }).ToList();

            var query = $"EXEC {ProcedureName} {string.Join(", ", parameters)},@choice='{choice}'";

            // Print or log the generated query
            Console.WriteLine("Generated Query: " + query); // You can replace Console.WriteLine with your preferred logging mechanism

            return query;
        }
        //Get Request
        public async Task<ActionResult<IEnumerable<T>>> Get()
        {
            var data = await _context.Set<T>().FromSqlRaw($"EXEC {ProcedureName} @choice='Get'").ToListAsync();
            //var data = await dbSet.ToListAsync();
            return Ok(data);
        }

        //Create Request
        public async Task<ActionResult<T>> Create(T entity)
        {
            var sqlQuery =  BuildSqlQuery(entity, "Insert");
            _context.Database.ExecuteSqlRaw(sqlQuery);
            //dbSet.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        //Update Request
        public async Task<IActionResult> Update(int id, T entity)
        {
            var entityType = typeof(T);
            var primaryKeyProperty = entityType.GetProperty("Id");

            if (primaryKeyProperty == null)
            {
                return BadRequest("Entity does not have a primary key property named 'Id'.");
            }

            var primaryKeyValue = primaryKeyProperty.GetValue(entity);

            if (id.ToString() != primaryKeyValue?.ToString())
            {
                return BadRequest();
            } 
            var sqlQuery = BuildSqlQuery(entity, "Update");
            Console.WriteLine(sqlQuery + "Update:");
            // Execute the SQL query
            _context.Database.ExecuteSqlRaw(sqlQuery);

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
            var data = await _context.Set<T>().FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            _context.Database.ExecuteSqlRaw($"EXEC {ProcedureName} @Id={id},@choice='Delete'");
            //dbSet.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }
    }
}
