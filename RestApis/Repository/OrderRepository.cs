using Microsoft.EntityFrameworkCore;
using RestApis.Models;
using RestApis.UnitofWork;

namespace RestApis.Repository
{
    public class OrderRepository : RepositoryBase<Order>
    {
        public OrderRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
