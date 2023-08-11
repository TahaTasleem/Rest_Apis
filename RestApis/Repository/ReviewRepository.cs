using Microsoft.EntityFrameworkCore;
using RestApis.Models;
using RestApis.UnitofWork;

namespace RestApis.Repository
{
    public class ReviewRepository : RepositoryBase<Review>
    {
        public ReviewRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
