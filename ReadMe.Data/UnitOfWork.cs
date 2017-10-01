using ReadMe.Data.Contracts;
using System.Threading.Tasks;

namespace ReadMe.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReadMeDbContext dbContext;

        public UnitOfWork(ReadMeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await this.dbContext.SaveChangesAsync();
        }
    }
}
