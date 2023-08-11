using Microsoft.EntityFrameworkCore;

namespace RestApis.UnitofWork
{
    public interface IUnitOfwork : IDisposable
    {
        DbContext Context { get; }
        public Task SaveChangesAsync();
    }
}
