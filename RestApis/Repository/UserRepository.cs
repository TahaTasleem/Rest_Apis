using Microsoft.EntityFrameworkCore;
using RestApis.Models;

namespace RestApis.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}
