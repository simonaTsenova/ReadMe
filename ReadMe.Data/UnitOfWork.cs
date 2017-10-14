using ReadMe.Data.Contracts;
using System;

namespace ReadMe.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IReadMeDbContext dbContext;

        public UnitOfWork(IReadMeDbContext dbContext)
        {
            if(dbContext == null)
            {
                throw new ArgumentNullException("Db context cannot be null");
            }

            this.dbContext = dbContext;
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}
