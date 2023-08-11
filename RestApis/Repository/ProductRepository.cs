using Microsoft.EntityFrameworkCore;
using RestApis.Models;
using RestApis.UnitofWork;

namespace RestApis.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
