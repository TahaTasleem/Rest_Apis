using Microsoft.EntityFrameworkCore;
using RestApis.Models;

namespace RestApis.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(DbContext context) : base(context)
        {
        }
    }
}
