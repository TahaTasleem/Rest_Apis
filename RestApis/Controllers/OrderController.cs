using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApis.Data;
using RestApis.Models;
using RestApis.Repository;

namespace RestApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        IRepository<Order> orderRepository;

        public OrderController(AppDbContext context)
        {
            _context = context;
            orderRepository = new OrderRepository(_context);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var orders = await orderRepository.Get();
            return orders;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var orders = await orderRepository.Create(order);
            return orders;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orders = await orderRepository.Delete(id);
            return orders;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            var orders = await orderRepository.Update(id, order);
            return orders;
        }


        /*//Get Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _context.Order.ToListAsync();
            return Ok(orders);
        }

        //Post Request
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }

        //Put Request
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var existingOrder = await _context.Order.FindAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            _context.Entry(existingOrder).CurrentValues.SetValues(order);

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
        public async Task<IActionResult> DeleteOrder (int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/
    }
   
}
