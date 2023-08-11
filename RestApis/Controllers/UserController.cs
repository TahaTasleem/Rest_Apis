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
    public class UserController : ControllerBase
    {
        private readonly IUnitOfwork _unitOfWork;
        IRepository<User> userRepository;
        protected DbSet<User> dbSet;

        public UserController(IUnitOfwork unitOfwork)
        {
            _unitOfWork = unitOfwork;
            dbSet = _unitOfWork.Context.Set<User>();
            userRepository = new UserRepository(_unitOfWork);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await userRepository.Get();
            return users;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var users = await userRepository.Delete(id);
            return users;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.Id)
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

            return NoContent();
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

        /*
        //Get Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.User.ToListAsync();
            return Ok(users);
        }

        //Put Request
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            _context.Entry(existingUser).CurrentValues.SetValues(user);

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
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/
    }
}
