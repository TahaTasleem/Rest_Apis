using Microsoft.EntityFrameworkCore;
using RestApis.Models;

namespace RestApis.Repository
{
    public class ReviewRepository : RepositoryBase<Review>
    {
        public ReviewRepository(DbContext context) : base(context)
        {
        }
    }
}
