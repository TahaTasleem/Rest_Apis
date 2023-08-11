using Microsoft.EntityFrameworkCore;
using RestApis.Models;
using RestApis.UnitofWork;

namespace RestApis.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
        {
        }
    }
}
