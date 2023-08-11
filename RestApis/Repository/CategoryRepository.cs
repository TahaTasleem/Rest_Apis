using Microsoft.EntityFrameworkCore;
using RestApis.Data;
using RestApis.Models;
using RestApis.UnitofWork;

namespace RestApis.Repository
{
    public class CategoryRepository : RepositoryBase<Category>
    {
        public CategoryRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
