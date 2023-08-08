using Microsoft.EntityFrameworkCore;
using RestApis.Data;
using RestApis.Models;

namespace RestApis.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }
    }
}
