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
            reviewRepository = new ReviewRepository(_unitOfWork,"crud4");
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
    }
}
