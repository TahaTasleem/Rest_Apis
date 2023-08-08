using Microsoft.EntityFrameworkCore;
using RestApis.Models;

namespace RestApis.Repository
{
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}
