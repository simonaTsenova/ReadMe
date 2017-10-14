using ReadMe.Data.Contracts;
using ReadMe.Models.Contracts;
using System;
using System.Linq;

namespace ReadMe.Data
{
    public class EfRepository<T> : IEfRepository<T>
        where T : class, IDeletable
    {
        private readonly IReadMeDbContext dbContext;

        public EfRepository(IReadMeDbContext dbContext)
        {
            if(dbContext == null)
            {
                throw new ArgumentNullException("DbContext cannot be null");
            }

            this.dbContext = dbContext;
        }

        public IQueryable<T> All
        {
            get
            {
                return this.dbContext.DbSet<T>().Where(x => !x.IsDeleted);
            }
        }

        public IQueryable<T> AllAndDeleted
        {
            get
            {
                return this.dbContext.DbSet<T>();
            }
        }

        public T GetById(object id)
        {
            return this.dbContext.DbSet<T>().Find(id);
        }

        public void Add(T entity)
        {
            this.dbContext.Add(entity);
        }

        public void Delete(T entity)
        {
            this.dbContext.Delete(entity);
        }

        public void Update(T entity)
        {
            this.dbContext.Update(entity);
        }
    }
}
