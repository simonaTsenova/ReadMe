using System.Data.Entity;
using System.Threading.Tasks;

namespace ReadMe.Data.Contracts
{
    public interface IReadMeDbContext
    {
        IDbSet<TEntity> DbSet<TEntity>() where TEntity : class;

        void Add<TEntry>(TEntry entity)
            where TEntry : class;

        void Delete<TEntry>(TEntry entity)
            where TEntry : class;

        void Update<TEntry>(TEntry entity)
            where TEntry : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
