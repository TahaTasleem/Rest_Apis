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
    [ApiController]
    [Route("[controller]")]
    [ApiExceptionHandler]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfWork;
        IRepository<User> userRepository;
        protected DbSet<User> dbSet;

        public UserController(IUnitOfwork unitOfwork)
        {
            _unitOfWork = unitOfwork;
            dbSet = _unitOfWork.Context.Set<User>();
            userRepository = new UserRepository(_unitOfWork,"crud5");
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await userRepository.Get();
            return users;
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var users = await userRepository.Delete(id);
            return users;
        }
        [Authorize]

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            var users = await userRepository.Update(id,user);
            return users;
            /*if (id != user.Id)
            {
                return BadRequest();
            }

            var existingEntity = await  dbSet.FindAsync(id);
            if (existingEntity == null)
            {
                return NotFound();
            }
            existingEntity.Username = user.Username;
            existingEntity.Email = user.Email;
            var originalPassword = existingEntity.Password;
            user.Password = originalPassword;
            _unitOfWork.Context.Entry(existingEntity).CurrentValues.SetValues(user);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            
            return NoContent();*/
        }

        //Post Request
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            string hashedPassword = HashPassword(user.Password);
            user.Password = hashedPassword;
            dbSet.Add(user);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
