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
    public class ReviewController : Controller
    {
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Review> reviewRepository;

        public ReviewController(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            reviewRepository = new ReviewRepository(_unitOfWork);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews()
        {
            var reviews = await reviewRepository.Get();
            return reviews;
        }

        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(Review review)
        {
            var reviews = await reviewRepository.Create(review);
            return reviews;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var reviews = await reviewRepository.Delete(id);
            return reviews;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            var reviews = await reviewRepository.Update(id, review);
            return reviews;
        }
        /*
        //Get Request
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetReviews()
        {
            var reviews = await _context.Review.ToListAsync();
            return Ok(reviews);
        }


        //Post Request
        [HttpPost]
        public async Task<ActionResult<Review>> CreateReview(Review review)
        {
            _context.Review.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviews), new { id = review.Id }, review);
        }

        //Put Request
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, Review review)
        {
            if (id != review.Id)
            {
                return BadRequest();
            }

            var existingReview = await _context.Review.FindAsync(id);
            if (existingReview == null)
            {
                return NotFound();
            }

            _context.Entry(existingReview).CurrentValues.SetValues(review);

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
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Review.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

    }
}
